using iTicket.Application.Bases;

namespace iTicket.Application.Features.BusSeats.Exceptions
{
    public class BusScheduleShouldBeExitsException() : BaseBadRequestException("Bus Schedule is invalid")
    {
    }


}
