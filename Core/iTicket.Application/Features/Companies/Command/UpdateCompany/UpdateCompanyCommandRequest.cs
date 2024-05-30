using MediatR;
using Microsoft.AspNetCore.Http;

namespace iTicket.Application.Features.Companies.Command.UpdateCompany
{
    public class UpdateCompanyCommandRequest : IRequest<Unit>
    {
        public int Id { get; init; }
        public string? Name { get; init; }
        public IFormFile? Logo { get; init; }
    }
}
