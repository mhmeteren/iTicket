using FluentValidation;

namespace iTicket.Application.Features.Admins.Command.SoftDeleteAdmin
{
    public class SoftDeleteAdminCommandValidator : AbstractValidator<SoftDeleteAdminCommandRequest>
    {
        public SoftDeleteAdminCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull();
        }
    }
}
