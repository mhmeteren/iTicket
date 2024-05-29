using FluentValidation;

namespace iTicket.Application.Features.Admins.Command.UpdateAdmin
{
    public class UpdateAdminCommandValidator : AbstractValidator<UpdateAdminCommandRequest>
    {
        public UpdateAdminCommandValidator()
        {

            RuleFor(x => x.Id).NotEmpty().NotNull();

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(100)
                .MinimumLength(8);
        }
    }
}
