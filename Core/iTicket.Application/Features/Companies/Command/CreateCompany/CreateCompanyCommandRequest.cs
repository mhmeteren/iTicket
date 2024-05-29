using MediatR;

namespace iTicket.Application.Features.Companies.Command.CreateCompany
{
    public record CreateCompanyCommandRequest : IRequest<Unit>
    {
        public string? Name { get; init; }
        public string? LogoUrl { get; init; }
    }
}
