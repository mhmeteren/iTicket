using iTicket.Application.Dtos.Payments;
using MediatR;

namespace iTicket.Application.Features.BusSeats.Command.CreateBusSeat
{
    public record CreateBusSeatCommandRequest : IRequest<Unit>
    {
        public int BusScheduleId { get; init; }
        public int StartBusRouteId { get; init; }
        public int EndBusRouteId { get; init; }

        public IList<PassangerDto>? Passangers { get; init; }

        public PaymentCardDto? PaymentCard { get; init; }

    }
}
