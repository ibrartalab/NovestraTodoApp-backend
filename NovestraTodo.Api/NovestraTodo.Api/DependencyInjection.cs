using Microsoft.Extensions.DependencyInjection;
using NovestraTodo.Application;
using NovestraTodo.Core;
using NovestraTodo.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovestraTodo.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAppDI(this IServiceCollection services,IConfiguration configuration)
        {
            // Register services here
            services.AddApplicationDI()
                .AddInfrastructureDI()
                .AddCoreDI(configuration);
            return services;
        }
    }
}