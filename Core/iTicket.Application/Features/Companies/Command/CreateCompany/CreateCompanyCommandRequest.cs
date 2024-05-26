using MediatR;

namespace iTicket.Application.Features.Companies.Command.CreateCompany
{
    public class CreateCompanyCommandRequest : IRequest<Unit>
    {
        public string? Name { get; set; }
        public string? LogoUrl { get; set; }
    }
}
