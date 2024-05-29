using iTicket.Application.Bases;
using iTicket.Application.Features.Auth.Rules;
using iTicket.Application.Interfaces.AutoMapper;
using iTicket.Application.Interfaces.UnitOfWorks;
using iTicket.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace iTicket.Application.Features.Admins.Command.CreateAdmin
{
    public class CreateAdminCommandHandler(
      IMapper mapper,
      IUnitOfWork unitOfWork,
      IHttpContextAccessor httpContextAccessor,
      AuthRules authRules,
      IdentityRules identityRules,
      UserManager<BaseUser> userManager)
      : BaseHandler(mapper, unitOfWork, httpContextAccessor), IRequestHandler<CreateAdminCommandRequest, Unit>
    {
        private readonly AuthRules authRules = authRules;
        private readonly IdentityRules identityRules = identityRules;
        private readonly UserManager<BaseUser> userManager = userManager;

        public async Task<Unit> Handle(CreateAdminCommandRequest request, CancellationToken cancellationToken)
        {
            await authRules.UserShouldNotBeExist(await userManager.FindByEmailAsync(request.Email));

            var admin = mapper.Map<Admin, CreateAdminCommandRequest>(request);
            admin.UserName = admin.Email;
            admin.EmailConfirmed = true;
            admin.SecurityStamp = Guid.NewGuid().ToString();

            IdentityResult result = await userManager.CreateAsync(admin, request.Password);
            await identityRules.IdentityResultValidation(result);

            await userManager.AddToRoleAsync(admin, "admin");

            return Unit.Value;
        }
    }
}
