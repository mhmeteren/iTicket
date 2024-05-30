using FluentValidation;

namespace iTicket.Application.Features.Companies.Command.UpdateCompany
{
    public class UpdateCompanyCommandValidator : AbstractValidator<UpdateCompanyCommandRequest>
    {
        public UpdateCompanyCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Name).NotEmpty().NotNull().MaximumLength(100).MinimumLength(5);
        }
    }
}
