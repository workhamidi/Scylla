using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Scylla.BLL.ModelsOperations.IOperations;
using Scylla.BLL.ModelsOperations.Operations;
using Scylla.DAL.Context;

namespace Scylla.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<AdventureWorksDW2019Context>(options =>
                options.UseSqlServer(configuration.GetConnectionString("MyConnectionString")));
            
            // DLL
            services.AddScoped<
                IDimEmployeeOperations,
                DimEmployeeOperations>();

            services.AddScoped<
                IFactSalesQuotumOperations,
                FactSalesQuotumOperations>();


            return services;
        }


    }
}