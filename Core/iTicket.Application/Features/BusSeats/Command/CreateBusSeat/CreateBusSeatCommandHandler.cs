using iTicket.Application.Bases;
using iTicket.Application.Dtos.Payments;
using iTicket.Application.Features.BusSeats.Rules;
using iTicket.Application.Interfaces.AutoMapper;
using iTicket.Application.Interfaces.Payments;
using iTicket.Application.Interfaces.UnitOfWorks;
using iTicket.Domain.Entities;
using Iyzipay.Model;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace iTicket.Application.Features.BusSeats.Command.CreateBusSeat
{
    public class CreateBusSeatCommandHandler(
        IMapper mapper,
        IUnitOfWork unitOfWork,
        IHttpContextAccessor httpContextAccessor,
        IPaymentService paymentService,
        BusRouteRules busRouteRules,
        BusScheduleRules busScheduleRules,
        PassangerRules passangerRules,
        PaymentRules paymentRules) : BaseHandler(mapper, unitOfWork, httpContextAccessor), IRequestHandler<CreateBusSeatCommandRequest, Unit>
    {
        private readonly IPaymentService paymentService = paymentService;
        private readonly BusRouteRules busRouteRules = busRouteRules;
        private readonly BusScheduleRules busScheduleRules = busScheduleRules;
        private readonly PassangerRules passangerRules = passangerRules;
        private readonly PaymentRules paymentRules = paymentRules;

        public async Task<Unit> Handle(CreateBusSeatCommandRequest request, CancellationToken cancellationToken)
        {

            Passenger? primaryPassenger = await unitOfWork.GetReadRepository<Passenger>().GetAsync(predicate: x => x.UserId == Guid.Parse(UserId) && x.Priority == 0);
            await passangerRules.PrimaryPassangerShouldBeExists(primaryPassenger);

            BusSchedule? busSchedule = await unitOfWork.GetReadRepository<BusSchedule>().GetAsync(predicate: x => x.Id == request.BusScheduleId && !x.IsDeleted, include: x => x.Include(i => i.BusRoutes));
            await busScheduleRules.BusScheduleShouldBeExits(busSchedule);

            BusRoute? startBusRoute = await unitOfWork.GetReadRepository<BusRoute>().GetAsync(predicate: x => x.Id == request.StartBusRouteId && x.BusScheduleId == request.BusScheduleId && !x.IsDeleted);
            await busRouteRules.BusRouteShouldBeExistsAndValid(startBusRoute);

            BusRoute? endBusRoute = await unitOfWork.GetReadRepository<BusRoute>().GetAsync(predicate: x => x.Id == request.EndBusRouteId && x.BusScheduleId == request.BusScheduleId && !x.IsDeleted);
            await busRouteRules.BusRouteShouldBeExistsAndValid(endBusRoute);

            int price = CalculatePrice(busSchedule.BusRoutes.ToList(), request.StartBusRouteId, request.EndBusRouteId);
            IList<BusSeat> busSeats = ParsingBusSeats(request, price);



            PaymentRequest paymentRequest = new(primaryPassenger: primaryPassenger, busSeats: busSeats, paymentCard: request.PaymentCard);
            Payment paymentResult = paymentService.CreatePaymentRequest(paymentRequest);
            await paymentRules.PaymentResultStatusShouldNotBeFailure(paymentResult.Status);


            await unitOfWork.GetWriteRepository<BusSeat>().AddRangeAsync(busSeats);
            await unitOfWork.SaveAsync();
            

            IList<PaymentTransaction> paymentTransactions = MapPaymentTransactions(paymentResult, busSeats);
            await unitOfWork.GetWriteRepository<PaymentTransaction>().AddRangeAsync(paymentTransactions);
            await unitOfWork.SaveAsync();

            //busSeats send SQS for Ticket notification
            return Unit.Value;
        }

        private IList<PaymentTransaction> MapPaymentTransactions(Payment paymentResult, IList<BusSeat> busSeats)
        {
            List<PaymentTransaction> paymentTransactions = [];
            for (int i = 0; i < paymentResult.PaymentItems.Count; i++)
            {
                paymentTransactions.Add(new()
                {
                    BusSeatId = busSeats[i].Id,
                    PaymentId = paymentResult.PaymentId,
                    PaymentTransactionId = paymentResult.PaymentItems[i].PaymentTransactionId,
                    Status = (int)Common.Enums.PaymentTransaction.Payment,
                    Price = decimal.Parse(paymentResult.PaymentItems[i].Price.Replace('.', ',')),
                    PaidPrice = decimal.Parse(paymentResult.PaymentItems[i].PaidPrice.Replace('.', ',')),
                    ConvertedPayoutPaidPrice = decimal.Parse(paymentResult.PaymentItems[i].ConvertedPayout.PaidPrice.Replace('.', ',')),
                    MerchantPayoutAmount = decimal.Parse(paymentResult.PaymentItems[i].MerchantPayoutAmount.Replace('.', ','))
                });
            }
            return paymentTransactions;
        }

        private int CalculatePrice(List<BusRoute> busRoutes, int StartBusRouteId, int EndBusRouteId)
        {
            busRoutes = busRoutes.OrderBy(i => i.Date).ToList();

            int startIndex = busRoutes.FindIndex(br => br.Id == StartBusRouteId);
            int endIndex = busRoutes.FindIndex(br => br.Id == EndBusRouteId);


            return busRoutes
                .Skip(startIndex + 1)
                .Take(endIndex - startIndex)
                .Sum(br => br.Price);
        }

        private IList<BusSeat> ParsingBusSeats(CreateBusSeatCommandRequest request, int price)
        {
            List<BusSeat> busSeats = [];

            foreach (PassangerDto passanger in request.Passangers)
            {
                busSeats.Add(new BusSeat()
                {
                    Status = (int)Common.Enums.PaymentTransaction.Payment,
                    SeatNumber = passanger.SeatNumber,
                    Price = price,
                    StartBusRouteId = request.StartBusRouteId,
                    EndBusRouteId = request.EndBusRouteId,
                    PassengerId = passanger.PassengerId,
                    BusScheduleId = request.BusScheduleId,

                });
            }
            return busSeats;
        }
    }



}
