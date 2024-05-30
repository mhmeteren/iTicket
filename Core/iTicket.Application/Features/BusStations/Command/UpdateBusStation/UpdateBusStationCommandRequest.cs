using iTicket.Application.Features.BusStations.Command.CreateBusStation;
using MediatR;

namespace iTicket.Application.Features.BusStations.Command.UpdateBusStation
{
    public record UpdateBusStationCommandRequest : IRequest<Unit>
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string City { get; init; }
    }


}
