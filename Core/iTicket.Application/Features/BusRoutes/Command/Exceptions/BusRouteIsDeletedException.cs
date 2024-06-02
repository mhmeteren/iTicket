using iTicket.Application.Bases;

namespace iTicket.Application.Features.BusRoutes.Command.Exceptions
{
    public class BusRouteIsDeletedException() : BaseNotFoundException("Bus Route not found")
    {
    }
}
