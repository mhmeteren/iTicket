using iTicket.Application.Bases;
using iTicket.Application.Features.BusSchedules.Rules;
using iTicket.Application.Interfaces.AutoMapper;
using iTicket.Application.Interfaces.UnitOfWorks;
using iTicket.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace iTicket.Application.Features.BusSchedules.Command.SoftDeleteBusSchedule
{
    public class SoftDeleteBusScheduleCommandHandler(
        IMapper mapper,
        IUnitOfWork unitOfWork,
        IHttpContextAccessor httpContextAccessor,
        BusScheduleRules busScheduleRules) : BaseHandler(mapper, unitOfWork, httpContextAccessor), IRequestHandler<SoftDeleteBusScheduleCommandRequest, Unit>


    {
        private readonly BusScheduleRules busScheduleRules = busScheduleRules;

        public async Task<Unit> Handle(SoftDeleteBusScheduleCommandRequest request, CancellationToken cancellationToken)
        {
            BusSchedule? busSchedule = await unitOfWork
                 .GetReadRepository<BusSchedule>()
                 .GetAsync(predicate: x => x.Id == request.Id && x.EmployeeId == Guid.Parse(UserId));

            await busScheduleRules.BusScheduleIsDeleted(busSchedule);

            int busSeatsCount = await unitOfWork
                .GetReadRepository<BusSeat>()
                .CountAsync(predicate: x => x.BusScheduleId == busSchedule.Id);

            await busScheduleRules.BusScheduleShouldNotHaveBusSeat(busSeatsCount);


            busSchedule.IsDeleted = true;
            await unitOfWork.GetWriteRepository<BusSchedule>().UpdateAsync(busSchedule);
            await unitOfWork.SaveAsync();

            IList<BusRoute> busRoutes = await unitOfWork
             .GetReadRepository<BusRoute>().GetAllAsync(predicate: x => !x.IsDeleted);
            foreach (BusRoute busRoute in busRoutes)
            {
                busRoute.IsDeleted = true;
            }
            await unitOfWork.GetWriteRepository<BusRoute>().UpdateRangeAsync(busRoutes);
            await unitOfWork.SaveAsync();
            return Unit.Value;
        }
    }
}
