using iTicket.Application.Bases;
using iTicket.Application.Features.Auth.Rules;
using iTicket.Application.Features.Employees.Rules;
using iTicket.Application.Interfaces.AutoMapper;
using iTicket.Application.Interfaces.UnitOfWorks;
using iTicket.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace iTicket.Application.Features.Employees.Command.UpdateEmployee
{
    public class UpdateEmployeeCommandHadnler(
      IMapper mapper,
      IUnitOfWork unitOfWork,
      IHttpContextAccessor httpContextAccessor,
      AuthRules authRules,
      IdentityRules identityRules,
      EmployeeRules employeeRules,
      UserManager<BaseUser> userManager)
      : BaseHandler(mapper, unitOfWork, httpContextAccessor), IRequestHandler<UpdateEmployeeCommandRequest, Unit>
    {
        private readonly AuthRules authRules = authRules;
        private readonly IdentityRules identityRules = identityRules;
        private readonly EmployeeRules employeeRules = employeeRules;
        private readonly UserManager<BaseUser> userManager = userManager;

        public async Task<Unit> Handle(UpdateEmployeeCommandRequest request, CancellationToken cancellationToken)
        {
            Employee? employee = await userManager.FindByIdAsync(request.Id) as Employee;
            await authRules.UserIsDeleted(employee);

            string? role = (await userManager.GetRolesAsync(employee)).FirstOrDefault();
            await authRules.UserRoleShouldBeEquals(role, "employee");

            await authRules.UserShouldNotBeExistIgnoreOwn(await userManager.FindByEmailAsync(request.Email), employee.Id);

            Company? company = await unitOfWork.GetReadRepository<Company>().GetAsync(predicate: x => x.Id == request.CompanyId);
            await employeeRules.CompanyShouldBeExist(company);

            mapper.Map(request, employee);
            employee.CompanyId = company.Id;
            employee.UserName = employee.Email;
            employee.EmailConfirmed = true;
            employee.RefreshToken = null;
            employee.RefreshTokenExpireTime = null;

            IdentityResult updateResult = await userManager.UpdateAsync(employee);
            await identityRules.IdentityResultValidation(updateResult);
            await userManager.UpdateSecurityStampAsync(employee);

            return Unit.Value;
        }
    }
}
