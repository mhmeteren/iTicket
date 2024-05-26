using FluentValidation;
using iTicket.Application.Beheviors;
using iTicket.Application.Exceptions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
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

            services.AddValidatorsFromAssembly(assembly);
            ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("en-US");
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(FluentValidationBehevior<,>));
        }

    }
}
