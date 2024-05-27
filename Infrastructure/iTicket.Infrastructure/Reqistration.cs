using iTicket.Application.Interfaces.Tokens;
using iTicket.Infrastructure.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace iTicket.Infrastructure
{
    public static class Reqistration
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var tokenSettings = configuration.GetSection("JWT");
            services.Configure<TokenSettings>(tokenSettings);
            services.AddTransient<ITokenService, TokenService>();

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt =>
            {
                opt.SaveToken = true;
                opt.TokenValidationParameters = new()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSettings["Secret"])),
                    ValidateLifetime = true,
                    ValidIssuer = tokenSettings["Issuer"],
                    ValidAudience = tokenSettings["Audience"],
                    ClockSkew = TimeSpan.Zero,
                };
            });

        }
    }
}
