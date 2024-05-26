using Microsoft.Extensions.DependencyInjection;

namespace iTicket.Mapper
{
    public static class Registration
    {
        public static void AddCustomMapper(this IServiceCollection services)
        {
            services.AddSingleton<Application.Interfaces.AutoMapper.IMapper, AutoMapper.Mapper>();
        }
    }
}
