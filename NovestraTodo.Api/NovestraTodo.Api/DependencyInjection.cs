using Microsoft.Extensions.DependencyInjection;
using NovestraTodo.Application;
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
        public static IServiceCollection AddAppDI(this IServiceCollection services)
        {
            // Register infrastructure services here
            services.AddApplicationDI()
                .AddInfrastructureDI();
            return services;
        }
    }
}