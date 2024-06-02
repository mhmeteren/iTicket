using MediatR;

namespace iTicket.Application.Features.BusRoutes.Command.CreateBusRoute
{
    public record CreateBusRouteCommandRequest : IRequest<Unit>
    {
        public int Price { get; init; }
        public int BusStationId { get; init; }
        public DateTime Date { get; init; }
    }
}
