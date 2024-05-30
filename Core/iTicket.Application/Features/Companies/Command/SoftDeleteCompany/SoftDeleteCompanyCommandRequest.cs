using MediatR;

namespace iTicket.Application.Features.Companies.Command.SoftDeleteCompany
{
    public record SoftDeleteCompanyCommandRequest : IRequest<Unit>
    {
        public int Id { get; init; }
    }
}
