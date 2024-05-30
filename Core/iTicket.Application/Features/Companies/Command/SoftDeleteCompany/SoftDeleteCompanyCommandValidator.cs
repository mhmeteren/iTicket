using FluentValidation;

namespace iTicket.Application.Features.Companies.Command.SoftDeleteCompany
{
    public class SoftDeleteCompanyCommandValidator : AbstractValidator<SoftDeleteCompanyCommandRequest>
    {
        public SoftDeleteCompanyCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull().GreaterThan(0);
        }
    }
}
