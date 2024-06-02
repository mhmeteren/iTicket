using iTicket.Application.Bases;
using iTicket.Application.Features.BusRoutes.Rules;
using iTicket.Application.Features.BusSchedules.Rules;
using iTicket.Application.Interfaces.AutoMapper;
using iTicket.Application.Interfaces.UnitOfWorks;
using iTicket.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace iTicket.Application.Features.BusRoutes.Command.SoftDeleteBusRoute
{
    public class SoftDeleteBusRouteCommandHandler(
        IMapper mapper,
        IUnitOfWork unitOfWork,
        IHttpContextAccessor httpContextAccessor,
        BusScheduleRules busScheduleRules,
        BusRouteRules busRouteRules) : BaseHandler(mapper, unitOfWork, httpContextAccessor), IRequestHandler<SoftDeleteBusRouteCommandRequest, Unit>
    {
        private readonly BusScheduleRules busScheduleRules = busScheduleRules;
        private readonly BusRouteRules busRouteRules = busRouteRules;

        public async Task<Unit> Handle(SoftDeleteBusRouteCommandRequest request, CancellationToken cancellationToken)
        {
            BusRoute? busRoute = await unitOfWork.GetReadRepository<BusRoute>().GetAsync(predicate: x => x.Id == request.Id);
            await busRouteRules.BusRouteIsDeleted(busRoute);

            BusSchedule? busSchedule = await unitOfWork
             .GetReadRepository<BusSchedule>()
             .GetAsync(predicate: x => x.Id == busRoute.BusScheduleId && x.EmployeeId == Guid.Parse(UserId));

            await busScheduleRules.BusScheduleIsDeleted(busSchedule);

            busRoute.IsDeleted = true;
            await unitOfWork.GetWriteRepository<BusRoute>().UpdateAsync(busRoute);
            await unitOfWork.SaveAsync();
            return Unit.Value;
        }
    }
}
