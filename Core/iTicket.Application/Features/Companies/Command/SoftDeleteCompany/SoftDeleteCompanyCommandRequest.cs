using MediatR;

namespace iTicket.Application.Features.Companies.Command.SoftDeleteCompany
{
    public class SoftDeleteCompanyCommandRequest : IRequest<Unit>
    {
        public int Id { get; init; }
    }
}
