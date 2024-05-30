using iTicket.Application.Bases;
using iTicket.Application.Features.Companies.Rules;
using iTicket.Application.Interfaces.AutoMapper;
using iTicket.Application.Interfaces.UnitOfWorks;
using iTicket.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace iTicket.Application.Features.Companies.Command.CreateCompany
{
    public class CreateCompanyCommandHandler(
        IMapper mapper,
        IUnitOfWork unitOfWork,
        IHttpContextAccessor httpContextAccessor,
        CompanyRules companyRules) : BaseHandler(mapper, unitOfWork, httpContextAccessor), IRequestHandler<CreateCompanyCommandRequest, Unit>
    {
        private readonly CompanyRules companyRules = companyRules;

        public async Task<Unit> Handle(CreateCompanyCommandRequest request, CancellationToken cancellationToken)
        {
            var companyCheck = await unitOfWork.GetReadRepository<Company>().GetAsync(x => x.Name.Equals(request.Name));
            await companyRules.CompanyNameMustBeUnique(companyCheck);

            Company company = mapper.Map<Company, CreateCompanyCommandRequest>(request);
            //Upload Logo and set LogoUrl
            company.LogoUrl = "test://url";
            await unitOfWork.GetWriteRepository<Company>().AddAsync(company);
            await unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}
