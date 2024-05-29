using FluentValidation;

namespace iTicket.Application.Features.Admins.Command.ResetPasswordAdmin
{
    public class ResetPasswordAdminCommandValidator : AbstractValidator<ResetPasswordAdminCommandRequest>
    {
        public ResetPasswordAdminCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull();

            RuleFor(x => x.NewPassword)
                .NotEmpty()
                .MinimumLength(8)
                .MaximumLength(15);

            RuleFor(x => x.ConfirmNewPassword)
                .NotEmpty()
                .Equal(x => x.NewPassword)
                .MinimumLength(8)
                .MaximumLength(15);
        }
    }

}
