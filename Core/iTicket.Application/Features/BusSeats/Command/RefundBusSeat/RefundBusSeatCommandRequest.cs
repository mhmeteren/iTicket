using MediatR;

namespace iTicket.Application.Features.BusSeats.Command.RefundBusSeat
{
    public record RefundBusSeatCommandRequest : IRequest<Unit>
    {
        public int BusSeatId { get; set; }
    }
}
