using iTicket.Application.Bases;
using iTicket.Application.Features.Auth.Rules;
using iTicket.Application.Interfaces.AutoMapper;
using iTicket.Application.Interfaces.Tokens;
using iTicket.Application.Interfaces.UnitOfWorks;
using iTicket.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;

namespace iTicket.Application.Features.Auth.Command.Login
{
    public class LoginCommandHandler(
          IMapper mapper,
          IUnitOfWork unitOfWork,
          IHttpContextAccessor httpContextAccessor,
          IConfiguration configuration,
          ITokenService tokenService,
          UserManager<BaseUser> userManager,
          AuthRules authRules)
          : BaseHandler(mapper, unitOfWork, httpContextAccessor), IRequestHandler<LoginCommandRequest, LoginCommandResponse>
    {
        private readonly IConfiguration configuration = configuration;
        private readonly ITokenService tokenService = tokenService;
        private readonly UserManager<BaseUser> userManager = userManager;
        private readonly AuthRules authRules = authRules;

        public async Task<LoginCommandResponse> Handle(LoginCommandRequest request, CancellationToken cancellationToken)
        {
            BaseUser? user = await userManager.FindByEmailAsync(request.Email);
            bool checkPassword = await userManager.CheckPasswordAsync(user, request.Password);
            await authRules.EmailOrPasswordShouldNotBeInvalid(user, checkPassword);

            IList<string> roles = await userManager.GetRolesAsync(user);

            JwtSecurityToken token = await tokenService.CreateToken(user, roles);
            string refreshToken = tokenService.GenerateRefreshToken();

            _ = int.TryParse(configuration["JWT:RefreshTokenValidityInDays"], out int RefreshTokenValidityInDays);

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpireTime = DateTime.Now.AddDays(RefreshTokenValidityInDays);
            await userManager.UpdateAsync(user);
            await userManager.UpdateSecurityStampAsync(user);

            string _token = new JwtSecurityTokenHandler().WriteToken(token);

            return new LoginCommandResponse()
            {
                Token = _token,
                RefreshToken = refreshToken,
                Expiration = token.ValidTo,
            };
        }
    }
}
