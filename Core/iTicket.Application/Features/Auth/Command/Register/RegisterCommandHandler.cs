using iTicket.Application.Bases;
using iTicket.Application.Features.Auth.Rules;
using iTicket.Application.Interfaces.AutoMapper;
using iTicket.Application.Interfaces.UnitOfWorks;
using iTicket.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace iTicket.Application.Features.Auth.Command.Register
{
    public class RegisterCommandHandler(
          IMapper mapper,
          IUnitOfWork unitOfWork,
          IHttpContextAccessor httpContextAccessor,
          AuthRules authRules,
          UserManager<BaseUser> userManager)
          : BaseHandler(mapper, unitOfWork, httpContextAccessor), IRequestHandler<RegisterCommandRequest, Unit>
    {
        private readonly AuthRules authRules = authRules;
        private readonly UserManager<BaseUser> userManager = userManager;

        public async Task<Unit> Handle(RegisterCommandRequest request, CancellationToken cancellationToken)
        {
            await authRules.UserShouldNotBeExist(await userManager.FindByEmailAsync(request.Email));

            var user = mapper.Map<User, RegisterCommandRequest>(request);
            user.UserName = user.Email;
            user.SecurityStamp = Guid.NewGuid().ToString();

            IdentityResult result = await userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "user");
            }

            return Unit.Value;
        }
    }
}
