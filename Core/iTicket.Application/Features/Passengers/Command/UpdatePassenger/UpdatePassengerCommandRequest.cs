using MediatR;

namespace iTicket.Application.Features.Passengers.Command.UpdatePassenger
{
    public record UpdatePassengerCommandRequest : IRequest<Unit>
    {
        public int Id { get; init; }
        public string? FullName { get; init; }
        public string? Gender { get; init; }
        public bool IsNotTurkishCitizen { get; init; }
        public string? IdentificationNo { get; init; }
        public string? PassportNo { get; init; }
        public string? Nationality { get; init; }

        public DateTime? DateOfBirth { get; init; }
    }


}
