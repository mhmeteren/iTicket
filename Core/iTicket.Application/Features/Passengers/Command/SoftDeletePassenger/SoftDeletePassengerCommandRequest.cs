using MediatR;

namespace iTicket.Application.Features.Passengers.Command.SoftDeletePassenger
{
    public record SoftDeletePassengerCommandRequest : IRequest<Unit>
    {
        public int Id { get; set; }
    }


}
