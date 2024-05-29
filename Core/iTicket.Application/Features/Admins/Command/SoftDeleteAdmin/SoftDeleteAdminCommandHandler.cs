using iTicket.Application.Bases;
using iTicket.Application.Features.Admins.Command.UpdateAdmin;
using iTicket.Application.Features.Auth.Rules;
using iTicket.Application.Interfaces.AutoMapper;
using iTicket.Application.Interfaces.UnitOfWorks;
using iTicket.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace iTicket.Application.Features.Admins.Command.SoftDeleteAdmin
{
    public class SoftDeleteAdminCommandHandler(
      IMapper mapper,
      IUnitOfWork unitOfWork,
      IHttpContextAccessor httpContextAccessor,
      AuthRules authRules,
      IdentityRules identityRules,
      UserManager<BaseUser> userManager)
      : BaseHandler(mapper, unitOfWork, httpContextAccessor), IRequestHandler<SoftDeleteAdminCommandRequest, Unit>
    {
        private readonly AuthRules authRules = authRules;
        private readonly IdentityRules identityRules = identityRules;
        private readonly UserManager<BaseUser> userManager = userManager;

        public async Task<Unit> Handle(SoftDeleteAdminCommandRequest request, CancellationToken cancellationToken)
        {
            BaseUser? admin = await userManager.FindByIdAsync(request.Id);
            await authRules.UserIsDeleted(admin);

            string? role = (await userManager.GetRolesAsync(admin)).FirstOrDefault();
            await authRules.UserRoleShouldBeEquals(role, "admin");

            admin.DeleteDate = DateTime.Now;
            admin.IsDeleted = true;
            admin.RefreshToken = null;
            admin.RefreshTokenExpireTime = null;

            IdentityResult updateResult = await userManager.UpdateAsync(admin);
            await identityRules.IdentityResultValidation(updateResult);
            await userManager.UpdateSecurityStampAsync(admin);

            return Unit.Value;
        }
    }
}
