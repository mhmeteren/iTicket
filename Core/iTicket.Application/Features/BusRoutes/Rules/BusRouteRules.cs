using iTicket.Application.Bases;
using iTicket.Application.Features.BusRoutes.Command.Exceptions;
using iTicket.Domain.Entities;

namespace iTicket.Application.Features.BusRoutes.Rules
{
    public class BusRouteRules : BaseRules
    {
        public Task BusRouteIsDeleted(BusRoute? busRoute)
        {
            if (busRoute is null || busRoute.IsDeleted) throw new BusRouteIsDeletedException();
            return Task.CompletedTask;
        }
    }

}
