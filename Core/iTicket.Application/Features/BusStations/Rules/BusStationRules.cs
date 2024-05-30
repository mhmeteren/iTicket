using iTicket.Application.Bases;
using iTicket.Application.Features.BusStations.Exceptions;
using iTicket.Domain.Entities;

namespace iTicket.Application.Features.BusStations.Rules
{
    public class BusStationRules : BaseRules
    {

        public Task BusStationShouldBeUnique(BusStation? busStation)
        {
            if (busStation is not null) throw new BusStationShouldBeUniqueException();
            return Task.CompletedTask;
        }

        public Task BusStationShouldBeUniqueIgnoreOwn(BusStation? busStation, int requestId)
        {
            if (busStation is not null && busStation.Id != requestId) throw new BusStationShouldBeUniqueException();
            return Task.CompletedTask;
        }

        public Task BusStationShouldBeExists(BusStation? busStation)
        {
            if (busStation is null || busStation.IsDeleted) throw new BusStationNotFoundException();
            return Task.CompletedTask;
        }
    }
}
