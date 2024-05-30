using iTicket.Application.Features.BusStations.Command.UpdateBusStation;
using MediatR;

namespace iTicket.Application.Features.BusStations.Command.SoftDeleteBusStation
{
    public record SoftDeleteBusStationCommandRequest : IRequest<Unit>
    {
        public int Id { get; init; }
    }


}
