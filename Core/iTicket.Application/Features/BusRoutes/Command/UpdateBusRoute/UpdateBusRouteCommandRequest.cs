using MediatR;

namespace iTicket.Application.Features.BusRoutes.Command.UpdateBusRoute
{
    public record UpdateBusRouteCommandRequest : IRequest<Unit>
    {
        public int Id { get; init; }
        public int Price { get; init; }
        public int BusStationId { get; init; }
        public DateTime Date { get; init; }
    }
}
