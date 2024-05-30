using iTicket.Application.Bases;

namespace iTicket.Application.Features.BusStations.Exceptions
{
    public class BusStationNotFoundException() : BaseNotFoundException("BusStation Not found.")
    {

    }
}
