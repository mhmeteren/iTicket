using iTicket.Application.Bases;

namespace iTicket.Application.Features.BusSeats.Exceptions
{
    public class PaymentTransactionShouldBeExitsException() : BaseBadRequestException("Payment Transaction not found or the ticket has already been cancelled.") { }
}
