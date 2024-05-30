using iTicket.Application.Bases;

namespace iTicket.Application.Features.BusStations.Exceptions
{
    public class BusStationShouldBeUniqueException() : BaseBadRequestException("Bus Station should be Unique")
    {
    }
}
