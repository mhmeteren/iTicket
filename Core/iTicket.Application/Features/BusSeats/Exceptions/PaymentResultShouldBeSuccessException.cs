using iTicket.Application.Bases;

namespace iTicket.Application.Features.BusSeats.Exceptions
{
    public class PaymentResultShouldBeSuccessException() : BaseBadRequestException("Payment could not be received")
    {
    }
}
