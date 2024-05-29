using MediatR;

namespace iTicket.Application.Features.Admins.Command.ResetPasswordAdmin
{
    public record ResetPasswordAdminCommandRequest: IRequest<Unit>
    {
        public string Id { get; init; }
        public string NewPassword { get; init; }
        public string ConfirmNewPassword { get; init; }
    }

}
