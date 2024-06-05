using iTicket.Application.Bases;

namespace iTicket.Application.Features.BusSeats.Exceptions
{
    public class RefundResultShouldBeSuccessException() : BaseBadRequestException("Refund could not be made, please try again later.")
    {
    }
}
