using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NovestraTodo.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            return services;
        }
    }
}
