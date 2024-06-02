using iTicket.Application.Bases;
using iTicket.Application.Features.BusRoutes.Command.UpdateBusRoute;
using iTicket.Application.Features.BusSchedules.Rules;
using iTicket.Application.Interfaces.AutoMapper;
using iTicket.Application.Interfaces.UnitOfWorks;
using iTicket.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace iTicket.Application.Features.BusSchedules.Command.UpdateBusSchedule
{
    public class UpdateBusScheduleCommandHandler(
        IMapper mapper,
        IUnitOfWork unitOfWork,
        IHttpContextAccessor httpContextAccessor,
        BusScheduleRules busScheduleRules) : BaseHandler(mapper, unitOfWork, httpContextAccessor), IRequestHandler<UpdateBusScheduleCommandRequest, Unit>

    {
        private readonly BusScheduleRules busScheduleRules = busScheduleRules;

        public async Task<Unit> Handle(UpdateBusScheduleCommandRequest request, CancellationToken cancellationToken)
        {
            BusSchedule? busSchedule = await unitOfWork
                .GetReadRepository<BusSchedule>()
                .GetAsync(predicate: x => x.Id == request.Id && x.EmployeeId == Guid.Parse(UserId));

            await busScheduleRules.BusScheduleIsDeleted(busSchedule);
            mapper.Map(request, busSchedule);
            await unitOfWork.GetWriteRepository<BusSchedule>().UpdateAsync(busSchedule);
            await unitOfWork.SaveAsync();

            IList<BusRoute> busRoutes = await unitOfWork
                .GetReadRepository<BusRoute>().GetAllAsync(predicate: x => !x.IsDeleted);

            IList<BusRoute> updateBusRoute = UpdateBusRouts(request.BusRouts, busRoutes);
            await unitOfWork.GetWriteRepository<BusRoute>().UpdateRangeAsync(updateBusRoute);
            await unitOfWork.SaveAsync();
            return Unit.Value;
        }

        private IList<BusRoute> UpdateBusRouts(IList<UpdateBusRouteCommandRequest> requestRoutes, IList<BusRoute> dbBusRoutes)
        {
            List<BusRoute> updateBusRoute = [];
            foreach (var requestRoute in requestRoutes)
            {
                BusRoute? findRoute =  dbBusRoutes.Where(x => x.Id == requestRoute.Id).FirstOrDefault();
                if(findRoute is not null)
                {
                    mapper.Map(requestRoute, findRoute);
                    updateBusRoute.Add(findRoute);
                }
            }
            return updateBusRoute;
        }
    }
}
