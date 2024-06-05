using iTicket.Application.Bases;
using iTicket.Application.Common.Constants;
using iTicket.Application.Features.BusSeats.Rules;
using iTicket.Application.Interfaces.AutoMapper;
using iTicket.Application.Interfaces.Payments;
using iTicket.Application.Interfaces.UnitOfWorks;
using iTicket.Domain.Entities;
using Iyzipay.Model;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace iTicket.Application.Features.BusSeats.Command.RefundBusSeat
{
    public class RefundBusSeatCommandHandler(
        IMapper mapper,
        IUnitOfWork unitOfWork,
        IHttpContextAccessor httpContextAccessor,
        IPaymentService paymentService,
        BusSeatRules busSeatRules,
        PaymentRules paymentRules) : BaseHandler(mapper, unitOfWork, httpContextAccessor), IRequestHandler<RefundBusSeatCommandRequest, Unit>

    {
        private readonly IPaymentService paymentService = paymentService;
        private readonly BusSeatRules busSeatRules = busSeatRules;
        private readonly PaymentRules paymentRules = paymentRules;

        public async Task<Unit> Handle(RefundBusSeatCommandRequest request, CancellationToken cancellationToken)
        {
            BusSeat? busSeat = await unitOfWork.GetReadRepository<BusSeat>()
                .GetAsync(
                include: x => x.Include(br => br.StartBusRoute).Include(ps => ps.Passenger),
                predicate: x => x.Id == request.BusSeatId && x.Passenger.UserId == Guid.Parse(UserId));

            await busSeatRules.BusSeatStartDateMustNotHaveExpired(busSeat);


            PaymentTransaction? paymentTransaction = await unitOfWork.GetReadRepository<PaymentTransaction>()
                .GetAsync(predicate: x => x.BusSeatId == busSeat.Id && x.Status == (int)Common.Enums.PaymentTransaction.Payment);
            await busSeatRules.PaymentTransactionShouldBeExits(paymentTransaction);

            if (BusRouteStartDateCheckForRefundPaidPrice(busSeat.StartBusRoute.Date))
            {
                RefundRequest refundRequest = new(price: paymentTransaction.PaidPrice, paymentTransactionId: paymentTransaction.PaymentTransactionId);
                Refund refundResult = paymentService.RefundPaymentRequest(refundRequest);
                await paymentRules.RefundResultStatusShouldNotBeFailure(refundResult.Status);

                paymentTransaction.Status = (int)Common.Enums.PaymentTransaction.Refund;
                busSeat.Status = (int)Common.Enums.PaymentTransaction.Refund;
            }
            else
            {
                paymentTransaction.Status = (int)Common.Enums.PaymentTransaction.Cancellation;
                busSeat.Status = (int)Common.Enums.PaymentTransaction.Refund;
            }

            paymentTransaction.TransactionLastUpdateDate = DateTime.Now;

            await unitOfWork.GetWriteRepository<BusSeat>().UpdateAsync(busSeat);
            await unitOfWork.GetWriteRepository<PaymentTransaction>().UpdateAsync(paymentTransaction);
            await unitOfWork.SaveAsync();
            return Unit.Value;
        }

        private bool BusRouteStartDateCheckForRefundPaidPrice(DateTime busRouteStartDate)
        {
            return (DateTime.Now.AddHours(ValueConstants.TicketCancellationHour) < busRouteStartDate);
        }
    }
}
