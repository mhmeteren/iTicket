using iTicket.Application.Bases;
using iTicket.Application.Features.Auth.Rules;
using iTicket.Application.Interfaces.AutoMapper;
using iTicket.Application.Interfaces.UnitOfWorks;
using iTicket.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace iTicket.Application.Features.Admins.Command.UpdateAdmin
{
    public class UpdateAdminCommandHandler(
      IMapper mapper,
      IUnitOfWork unitOfWork,
      IHttpContextAccessor httpContextAccessor,
      AuthRules authRules,
      IdentityRules identityRules,
      UserManager<BaseUser> userManager)
      : BaseHandler(mapper, unitOfWork, httpContextAccessor), IRequestHandler<UpdateAdminCommandRequest, Unit>
    {
        private readonly IdentityRules identityRules = identityRules;

        public async Task<Unit> Handle(UpdateAdminCommandRequest request, CancellationToken cancellationToken)
        {
            BaseUser? admin = await userManager.FindByIdAsync(request.Id);
            await authRules.UserIsDeleted(admin);

            string? role = (await userManager.GetRolesAsync(admin)).FirstOrDefault();
            await authRules.UserRoleShouldBeEquals(role, "admin");

            await authRules.UserShouldNotBeExistIgnoreOwn(await userManager.FindByEmailAsync(request.Email), admin.Id);


            mapper.Map(request, admin);
            admin.UserName = admin.Email;
            admin.EmailConfirmed = true;
            admin.RefreshToken = null;
            admin.RefreshTokenExpireTime = null;

            IdentityResult updateResult = await userManager.UpdateAsync(admin);
            await identityRules.IdentityResultValidation(updateResult);
            await userManager.UpdateSecurityStampAsync(admin);
            return Unit.Value;
        }
    }
}
