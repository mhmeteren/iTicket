using iTicket.Application.Bases;
using iTicket.Application.Features.Companies.Rules;
using iTicket.Application.Interfaces.AutoMapper;
using iTicket.Application.Interfaces.UnitOfWorks;
using iTicket.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace iTicket.Application.Features.Companies.Command.SoftDeleteCompany
{
    public class SoftDeleteCompanyCommandHandler(
        IMapper mapper,
        IUnitOfWork unitOfWork,
        IHttpContextAccessor httpContextAccessor,
        CompanyRules companyRules) : BaseHandler(mapper, unitOfWork, httpContextAccessor), IRequestHandler<SoftDeleteCompanyCommandRequest, Unit>

    {
        private readonly CompanyRules companyRules = companyRules;

        public async Task<Unit> Handle(SoftDeleteCompanyCommandRequest request, CancellationToken cancellationToken)
        {
            var company = await unitOfWork.GetReadRepository<Company>().GetAsync(x => x.Id == request.Id);
            await companyRules.CompanyIsDeleted(company);

            IList<Employee> employees = await unitOfWork
                .GetReadRepository<Employee>().GetAllAsync(predicate: x => x.CompanyId == company.Id);

            IList<Employee> DeleteEmployees = RevokeAndDeleteEmployee(employees);

            company.IsDeleted = true;
            await unitOfWork.GetWriteRepository<Company>().UpdateAsync(company);
            await unitOfWork.GetWriteRepository<Employee>().UpdateRangeAsync(employees);
            //IsDelete BusSchedules and Cancel User Tickets
            await unitOfWork.SaveAsync();

            return Unit.Value;
        }

        private IList<Employee> RevokeAndDeleteEmployee(IList<Employee> employees)
        {

            foreach (var employee in employees)
            {
                employee.IsDeleted = true;
                employee.DeleteDate = DateTime.Now;
                employee.RefreshToken = null;
                employee.RefreshTokenExpireTime = null;
            }

            return employees;
        }
    }
}
