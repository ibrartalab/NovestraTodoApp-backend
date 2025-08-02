using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NovestraTodo.Core.Interfaces;
using NovestraTodo.Infrastructure.Data;


namespace NovestraTodo.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureDI(this IServiceCollection services)
        {
            services.AddDbContext<NovestraDbContext>(options =>
            {
                options.UseSqlServer("Server=.;Database=NovestraTodos;Trusted_Connection=True;TrustServerCertificate=true;MultipleActiveResultSets=true;");
            });

            services.AddScoped<IUserRepository, IUserRepository>();
            return services;
        }
    }
}
