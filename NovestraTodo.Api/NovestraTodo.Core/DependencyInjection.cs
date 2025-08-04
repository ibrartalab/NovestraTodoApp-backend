using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NovestraTodo.Core.Options;


namespace NovestraTodo.Core
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCoreDI(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ConnectionStringOptions>(configuration.GetSection(ConnectionStringOptions.ConnectionString));
            return services;
        }
    }
}