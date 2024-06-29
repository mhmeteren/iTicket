using iTicket.Application.Interfaces.Payments;
using iTicket.Application.Interfaces.RedisCache;
using iTicket.Application.Interfaces.Tokens;
using iTicket.Infrastructure.Payments;
using iTicket.Infrastructure.RedisCache;
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


            services.Configure<Iyzipay.Options>(configuration.GetSection("Payment"));
            services.AddTransient<IPaymentService, IyzipayPaymentService>();

            var redisCacheSettings = configuration.GetSection("RedisCacheSettings");
            services.Configure<RedisCacheSettings>(redisCacheSettings);
            services.AddTransient<IRedisCacheService, RedisCacheService>();

            services.AddStackExchangeRedisCache(opt =>
            {
                opt.Configuration = redisCacheSettings["ConnectionString"];
                opt.InstanceName = redisCacheSettings["InstanceName"];

            });

        }
    }
}
