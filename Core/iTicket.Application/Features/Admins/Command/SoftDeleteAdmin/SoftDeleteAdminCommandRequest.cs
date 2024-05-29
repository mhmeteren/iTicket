using MediatR;

namespace iTicket.Application.Features.Admins.Command.SoftDeleteAdmin
{
    public record SoftDeleteAdminCommandRequest : IRequest<Unit>
    {
        public string Id { get; init; }
    }
}
