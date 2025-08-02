using Microsoft.Extensions.DependencyInjection;

namespace NovestraTodo.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationDI(this IServiceCollection services)
        {
            services.AddMediatR(conf => conf.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));
            return services;
        }
    }

}
