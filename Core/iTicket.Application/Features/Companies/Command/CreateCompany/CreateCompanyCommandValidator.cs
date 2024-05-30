using FluentValidation;

namespace iTicket.Application.Features.Companies.Command.CreateCompany
{
    public class CreateCompanyCommandValidator : AbstractValidator<CreateCompanyCommandRequest>
    {
        public CreateCompanyCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull().MaximumLength(100).MinimumLength(5);
            RuleFor(x => x.Logo).NotEmpty().NotNull();
        }
    }
}
