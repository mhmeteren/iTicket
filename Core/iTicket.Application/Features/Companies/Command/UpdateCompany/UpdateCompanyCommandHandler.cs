using iTicket.Application.Bases;
using iTicket.Application.Features.Companies.Rules;
using iTicket.Application.Interfaces.AutoMapper;
using iTicket.Application.Interfaces.UnitOfWorks;
using iTicket.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace iTicket.Application.Features.Companies.Command.UpdateCompany
{
    public class UpdateCompanyCommandHandler(
        IMapper mapper,
        IUnitOfWork unitOfWork,
        IHttpContextAccessor httpContextAccessor,
        CompanyRules companyRules) : BaseHandler(mapper, unitOfWork, httpContextAccessor), IRequestHandler<UpdateCompanyCommandRequest, Unit>
    {
        private readonly CompanyRules companyRules = companyRules;

        public async Task<Unit> Handle(UpdateCompanyCommandRequest request, CancellationToken cancellationToken)
        {
            var company = await unitOfWork.GetReadRepository<Company>().GetAsync(x => x.Id == request.Id);
            await companyRules.CompanyIsDeleted(company);

            var companyCheck = await unitOfWork.GetReadRepository<Company>().GetAsync(x => x.Name.Equals(request.Name));
            await companyRules.CompanyNameShouldNotBeExistIgnoreOwn(companyCheck, company.Id);

            mapper.Map(request, company);
            //Upload Logo and set LogoUrl
            await unitOfWork.GetWriteRepository<Company>().UpdateAsync(company);
            await unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}
