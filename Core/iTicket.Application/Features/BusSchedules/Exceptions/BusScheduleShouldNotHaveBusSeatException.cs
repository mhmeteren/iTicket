using iTicket.Application.Bases;

namespace iTicket.Application.Features.BusSchedules.Exceptions
{
    public class BusScheduleShouldNotHaveBusSeatException() : BaseBadRequestException("Bus Schedule Shouldn't Have Bus Seat") { }
}
