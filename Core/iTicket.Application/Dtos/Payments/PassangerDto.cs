namespace iTicket.Application.Dtos.Payments
{
    public record PassangerDto
    {
        public int SeatNumber { get; init; }
        public int PassengerId { get; init; }
    }
}
