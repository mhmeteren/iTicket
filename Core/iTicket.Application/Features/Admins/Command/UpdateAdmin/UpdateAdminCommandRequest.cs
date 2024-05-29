using MediatR;

namespace iTicket.Application.Features.Admins.Command.UpdateAdmin
{
    public record UpdateAdminCommandRequest : IRequest<Unit>
    {
        public string Id { get; init; }
        public string Email { get; init; }

    }
}
