using iTicket.Application.Features.BusRoutes.Command.CreateBusRoute;
using MediatR;

namespace iTicket.Application.Features.BusSchedules.Command.CreateBusSchedule
{
    public record CreateBusScheduleCommandRequest: IRequest<Unit>
    {
        public int TripType { get; init; }
        public int SeatCount { get; init; }

        public IList<CreateBusRouteCommandRequest>? BusRouts { get; init; }
    }

}
