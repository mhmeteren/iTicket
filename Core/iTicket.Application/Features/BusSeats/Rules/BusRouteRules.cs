using iTicket.Application.Bases;
using iTicket.Application.Features.BusSeats.Exceptions;
using iTicket.Domain.Entities;

namespace iTicket.Application.Features.BusSeats.Rules
{
    public class BusRouteRules : BaseRules
    {
        public Task BusRouteShouldBeExistsAndValid(BusRoute? busRoute)
        {
            if (busRoute is null || busRoute.IsDeleted || busRoute.Date < DateTime.Now)
                throw new BusRouteShouldBeExistsAndValidException();
            return Task.CompletedTask;
        }
    }
}
