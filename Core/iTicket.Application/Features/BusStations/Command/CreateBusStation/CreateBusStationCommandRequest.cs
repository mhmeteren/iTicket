using MediatR;

namespace iTicket.Application.Features.BusStations.Command.CreateBusStation
{
    public record CreateBusStationCommandRequest : IRequest<Unit>
    {
        public string Name { get; init; }
        public string City { get; init; }
    }
}
