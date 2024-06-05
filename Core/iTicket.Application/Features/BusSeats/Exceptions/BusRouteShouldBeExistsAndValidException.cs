using iTicket.Application.Bases;

namespace iTicket.Application.Features.BusSeats.Exceptions
{
    public class BusRouteShouldBeExistsAndValidException() : BaseBadRequestException("Bus route is invalid")
    {
    }   


}
