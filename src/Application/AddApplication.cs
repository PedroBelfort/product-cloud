using Application.Interfaces;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public static class AddApplication
    {
        public static IServiceCollection AddApplicationDI (this IServiceCollection services )
        {
            services.AddScoped<IProductService, ProductService>();
            return services;
        }

    }
}
