using iTicket.Application.Bases;
using iTicket.Application.Features.Auth.Rules;
using iTicket.Application.Features.Employees.Command.UpdateEmployee;
using iTicket.Application.Features.Employees.Rules;
using iTicket.Application.Interfaces.AutoMapper;
using iTicket.Application.Interfaces.UnitOfWorks;
using iTicket.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace iTicket.Application.Features.Employees.Command.SoftDeleteEmployee
{
    public class SoftDeleteEmployeeCommandHandler(
      IMapper mapper,
      IUnitOfWork unitOfWork,
      IHttpContextAccessor httpContextAccessor,
      AuthRules authRules,
      IdentityRules identityRules,
      UserManager<BaseUser> userManager)
      : BaseHandler(mapper, unitOfWork, httpContextAccessor), IRequestHandler<SoftDeleteEmployeeCommandRequest, Unit>
    {
        private readonly AuthRules authRules = authRules;
        private readonly IdentityRules identityRules = identityRules;
        private readonly UserManager<BaseUser> userManager = userManager;

        public async Task<Unit> Handle(SoftDeleteEmployeeCommandRequest request, CancellationToken cancellationToken)
        {
            Employee? employee = await userManager.FindByIdAsync(request.Id) as Employee;
            await authRules.UserIsDeleted(employee);

            string? role = (await userManager.GetRolesAsync(employee)).FirstOrDefault();
            await authRules.UserRoleShouldBeEquals(role, "employee");

            employee.IsDeleted = true;
            employee.DeleteDate = DateTime.Now;
            employee.RefreshToken = null;
            employee.RefreshTokenExpireTime = null;

            IdentityResult updateResult = await userManager.UpdateAsync(employee);
            await identityRules.IdentityResultValidation(updateResult);
            await userManager.UpdateSecurityStampAsync(employee);

            return Unit.Value;
        }
    }
}
