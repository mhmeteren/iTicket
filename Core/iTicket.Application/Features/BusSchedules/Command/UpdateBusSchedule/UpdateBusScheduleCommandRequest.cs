using iTicket.Application.Features.BusRoutes.Command.UpdateBusRoute;
using iTicket.Application.Features.BusSchedules.Command.CreateBusSchedule;
using iTicket.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace iTicket.Application.Features.BusSchedules.Command.UpdateBusSchedule
{
    public record UpdateBusScheduleCommandRequest : IRequest<Unit>
    {
        public int Id { get; init; }
        public int TripType { get; init; }
        public int SeatCount { get; init; }

        public IList<UpdateBusRouteCommandRequest>? BusRouts { get; init; }
    }
}
