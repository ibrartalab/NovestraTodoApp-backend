using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NovestraTodo.Core.Interfaces;
using NovestraTodo.Core.Options;
using NovestraTodo.Infrastructure.Data;
using NovestraTodo.Infrastructure.Repositories;


namespace NovestraTodo.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureDI(this IServiceCollection services)
        {
            services.AddDbContext<NovestraDbContext>((provider,options) =>
            {
                options.UseSqlServer(provider.GetRequiredService<IOptionsSnapshot<ConnectionStringOptions>>().Value.DefaultConnection;
            });

            services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }
    }
}
