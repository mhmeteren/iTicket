using iTicket.Application.Bases;
using iTicket.Application.Features.Auth.Rules;
using iTicket.Application.Interfaces.AutoMapper;
using iTicket.Application.Interfaces.UnitOfWorks;
using iTicket.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace iTicket.Application.Features.Auth.Command.Revoke
{
    public class RevokeCommandHandler(
    IMapper mapper,
    IUnitOfWork unitOfWork,
    IHttpContextAccessor httpContextAccessor,
    UserManager<BaseUser> userManager,
    AuthRules authRules)
    : BaseHandler(mapper, unitOfWork, httpContextAccessor), IRequestHandler<RevokeCommandRequest, Unit>
    {
        private readonly UserManager<BaseUser> userManager = userManager;
        private readonly AuthRules authRules = authRules;

        public async Task<Unit> Handle(RevokeCommandRequest request, CancellationToken cancellationToken)
        {

            BaseUser? user = await userManager.FindByIdAsync(UserId);
            await authRules.UserShouldBeValid(user);
            await authRules.SessionAlreadyRevoked(user);

            user.RefreshToken = null;
            user.RefreshTokenExpireTime = null;
            await userManager.UpdateAsync(user);

            return Unit.Value;
        }
    }
}
