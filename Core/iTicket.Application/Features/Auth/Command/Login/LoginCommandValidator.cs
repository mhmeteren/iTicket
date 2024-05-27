using FluentValidation;

namespace iTicket.Application.Features.Auth.Command.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommandRequest>
    {
        public LoginCommandValidator()
        {
            RuleFor(x => x.Email)
             .NotEmpty()
             .EmailAddress()
             .MaximumLength(100)
             .MinimumLength(8);

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(8)
                .MaximumLength(15);

        }
    }
}
