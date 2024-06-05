using iTicket.Application.Bases;

namespace iTicket.Application.Features.BusSeats.Exceptions
{
    public class BusSeatStartDateMustNotHaveExpiredException() : BaseBadRequestException("The Start Date of the Bus Seat must not have expired") { }
}
