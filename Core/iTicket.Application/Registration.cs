using iTicket.Application.Exceptions;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace iTicket.Application
{
    public static class Registration
    {

        public static void AddApplication(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();
            services.AddMediatR(conf => conf.RegisterServicesFromAssembly(assembly));
            services.AddTransient<ExceptionMiddleware>();
        }

    }
}
