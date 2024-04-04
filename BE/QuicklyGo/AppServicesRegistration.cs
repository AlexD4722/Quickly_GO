using MediatR;
using System.Reflection;

namespace QuicklyGo
{
    public static class AppServicesRegistration
    {
        public static IServiceCollection ConfigureAppServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
