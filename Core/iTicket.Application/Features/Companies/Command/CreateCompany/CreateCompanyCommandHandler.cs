using iTicket.Application.Bases;
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
        IHttpContextAccessor httpContextAccessor) : BaseHandler(mapper, unitOfWork, httpContextAccessor), IRequestHandler<CreateCompanyCommandRequest, Unit>
    {
        public async Task<Unit> Handle(CreateCompanyCommandRequest request, CancellationToken cancellationToken)
        {
            var companyCheck = await unitOfWork.GetReadRepository<Company>().GetAsync(x => x.Name.Equals(request.Name));

            if (companyCheck != null) { /*write rule*/}

            Company company = mapper.Map<Company, CreateCompanyCommandRequest>(request);
            await unitOfWork.GetWriteRepository<Company>().AddAsync(company);
            await unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}
