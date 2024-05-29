using MediatR;

namespace iTicket.Application.Features.Admins.Command.CreateAdmin
{
    public record CreateAdminCommandRequest : IRequest<Unit>
    {
        public string Email { get; init; }
        public string Password { get; init; }
        public string ConfirmPassword { get; init; }

    }
}
