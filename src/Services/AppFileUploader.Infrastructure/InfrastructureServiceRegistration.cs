using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppFileUploader.Application.Contract.Persistence;
using AppFileUploader.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AppFileUploader.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppManagementDbContext>(
                options => options.UseMySql(
                    configuration.GetConnectionString("MySQLDB"),
                    ServerVersion.AutoDetect(configuration.GetConnectionString("MySQLDB"))
                )
            );

            services.AddScoped(typeof(IFileContent<>), typeof(FileContentRepo<>));
            
            return services;
        }
    }
}