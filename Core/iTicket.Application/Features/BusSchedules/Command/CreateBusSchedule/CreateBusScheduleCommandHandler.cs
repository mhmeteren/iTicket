using iTicket.Application.Bases;
using iTicket.Application.Features.BusRoutes.Command.CreateBusRoute;
using iTicket.Application.Features.BusSchedules.Rules;
using iTicket.Application.Interfaces.AutoMapper;
using iTicket.Application.Interfaces.UnitOfWorks;
using iTicket.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace iTicket.Application.Features.BusSchedules.Command.CreateBusSchedule
{
    public class CreateBusScheduleCommandHandler(
        IMapper mapper,
        IUnitOfWork unitOfWork,
        IHttpContextAccessor httpContextAccessor,
        UserManager<BaseUser> userManager,
        BusScheduleRules busScheduleRules) : BaseHandler(mapper, unitOfWork, httpContextAccessor), IRequestHandler<CreateBusScheduleCommandRequest, Unit>

    {
        private readonly UserManager<BaseUser> userManager = userManager;
        private readonly BusScheduleRules busScheduleRules = busScheduleRules;

        public async Task<Unit> Handle(CreateBusScheduleCommandRequest request, CancellationToken cancellationToken)
        {
            Employee? employee = await userManager.FindByIdAsync(UserId) as Employee;
            await busScheduleRules.OwnerEmployeeIsDeleted(employee);

            BusSchedule busSchedule = mapper.Map<BusSchedule, CreateBusScheduleCommandRequest>(request);
            busSchedule.EmployeeId = employee.Id;
            busSchedule.CompanyId = employee.CompanyId;

            await unitOfWork.GetWriteRepository<BusSchedule>().AddAsync(busSchedule);
            await unitOfWork.SaveAsync();

            IList<BusRoute> busRoutes = mapper.Map<BusRoute, CreateBusRouteCommandRequest>(request.BusRouts);
            
            busRoutes = busRoutes.OrderBy(x => x.Date).ToList();
            busRoutes.FirstOrDefault().Price = 0;

            foreach (BusRoute route in busRoutes)
            {
                route.BusScheduleId = busSchedule.Id;
            }

            await unitOfWork.GetWriteRepository<BusRoute>().AddRangeAsync(busRoutes);
            await unitOfWork.SaveAsync();
            return Unit.Value;
        }
    }

}
