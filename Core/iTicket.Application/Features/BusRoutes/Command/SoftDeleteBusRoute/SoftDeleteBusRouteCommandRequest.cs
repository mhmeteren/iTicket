using MediatR;

namespace iTicket.Application.Features.BusRoutes.Command.SoftDeleteBusRoute
{
    public record SoftDeleteBusRouteCommandRequest : IRequest<Unit>
    {
        public int Id { get; init; }
    }
}
