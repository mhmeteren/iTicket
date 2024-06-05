namespace iTicket.Application.Dtos.Payments
{
    public record PaymentCardDto
    {
        public string CardHolderName { get; init; }
        public string CardNumber { get; init; }
        public string ExpireYear { get; init; }
        public string ExpireMonth { get; init; }
        public string Cvc { get; init; }
    }
}
