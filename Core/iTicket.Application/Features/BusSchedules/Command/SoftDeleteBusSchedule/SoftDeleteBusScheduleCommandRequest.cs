using MediatR;

namespace iTicket.Application.Features.BusSchedules.Command.SoftDeleteBusSchedule
{
    public record SoftDeleteBusScheduleCommandRequest : IRequest<Unit>
    {
        public int Id { get; init; }
    }
}
