using FluentValidation;

namespace iTicket.Application.Features.Admins.Command.CreateAdmin
{
    public class CreateAdminCommandValidator : AbstractValidator<CreateAdminCommandRequest>
    {
        public CreateAdminCommandValidator()
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

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty()
                .Equal(x => x.Password)
                .MinimumLength(8)
                .MaximumLength(15);
        }
    }
}
