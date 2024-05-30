using iTicket.Application.Bases;
using iTicket.Application.Features.Auth.Rules;
using iTicket.Application.Features.Employees.Rules;
using iTicket.Application.Interfaces.AutoMapper;
using iTicket.Application.Interfaces.UnitOfWorks;
using iTicket.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace iTicket.Application.Features.Employees.Command.CreateEmployee
{
    public class CreateEmployeeCommandHandler(
      IMapper mapper,
      IUnitOfWork unitOfWork,
      IHttpContextAccessor httpContextAccessor,
      AuthRules authRules,
      IdentityRules identityRules,
      EmployeeRules employeeRules,
      UserManager<BaseUser> userManager)
      : BaseHandler(mapper, unitOfWork, httpContextAccessor), IRequestHandler<CreateEmployeeCommandRequest, Unit>
    {
        private readonly AuthRules authRules = authRules;
        private readonly IdentityRules identityRules = identityRules;
        private readonly EmployeeRules employeeRules = employeeRules;
        private readonly UserManager<BaseUser> userManager = userManager;

        public async Task<Unit> Handle(CreateEmployeeCommandRequest request, CancellationToken cancellationToken)
        {
            await authRules.UserShouldNotBeExist(await userManager.FindByEmailAsync(request.Email));

            Company? company = await unitOfWork.GetReadRepository<Company>().GetAsync(predicate: x => x.Id == request.CompanyId);
            await employeeRules.CompanyShouldBeExist(company);

            Employee employee = mapper.Map<Employee, CreateEmployeeCommandRequest>(request);
            employee.CompanyId = company.Id;
            employee.UserName = employee.Email;
            employee.EmailConfirmed = true;
            employee.SecurityStamp = Guid.NewGuid().ToString();


            IdentityResult result = await userManager.CreateAsync(employee, request.Password);
            await identityRules.IdentityResultValidation(result);

            await userManager.AddToRoleAsync(employee, "employee");

            return Unit.Value;
        }
    }
}
