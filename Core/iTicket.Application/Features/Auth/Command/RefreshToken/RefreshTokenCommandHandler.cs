using iTicket.Application.Bases;
using iTicket.Application.Features.Auth.Rules;
using iTicket.Application.Interfaces.AutoMapper;
using iTicket.Application.Interfaces.Tokens;
using iTicket.Application.Interfaces.UnitOfWorks;
using iTicket.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace iTicket.Application.Features.Auth.Command.RefreshToken
{
    public class RefreshTokenCommandHandler(
           IMapper mapper,
           IUnitOfWork unitOfWork,
           IHttpContextAccessor httpContextAccessor,
           ITokenService tokenService,
           UserManager<BaseUser> userManager,
           AuthRules authRules)
           : BaseHandler(mapper, unitOfWork, httpContextAccessor), IRequestHandler<RefreshTokenCommandRequest, RefreshTokenCommandResponse>
    {
        private readonly ITokenService tokenService = tokenService;
        private readonly UserManager<BaseUser> userManager = userManager;
        private readonly AuthRules authRules = authRules;

        public async Task<RefreshTokenCommandResponse> Handle(RefreshTokenCommandRequest request, CancellationToken cancellationToken)
        {
            ClaimsPrincipal? principal = tokenService.GetPrincipalFromExpiredToken(request.Token);
            string email = principal.FindFirstValue(ClaimTypes.Email);

            BaseUser? user = await userManager.FindByEmailAsync(email);
            await authRules.RefreshTokenCheck(user?.RefreshToken, request.RefreshToken);
            await authRules.RefreshTokenShoulNotBeExpired(user.RefreshTokenExpireTime);


            IList<string> roles = await userManager.GetRolesAsync(user);
            JwtSecurityToken jwtSecurityToken = await tokenService.CreateToken(user, roles);
            string refreshToken = tokenService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            await userManager.UpdateAsync(user);
            await userManager.UpdateSecurityStampAsync(user);

            string _token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return new()
            {
                Token = _token,
                RefreshToken = refreshToken,
                Expiration = jwtSecurityToken.ValidTo
            };
        }
    }
}
