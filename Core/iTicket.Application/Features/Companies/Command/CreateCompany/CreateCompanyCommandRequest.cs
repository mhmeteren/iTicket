using MediatR;
using Microsoft.AspNetCore.Http;

namespace iTicket.Application.Features.Companies.Command.CreateCompany
{
    public record CreateCompanyCommandRequest : IRequest<Unit>
    {
        public string? Name { get; init; }
        public IFormFile? Logo {  get; init; }
    }
}
