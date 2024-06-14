using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AppFileUploader.Application.Contract.Persistence;
using AppFileUploader.Application.Contract.Storage;
using AppFileUploader.Infrastructure.Persistence;
using AppFileUploader.Infrastructure.Storage.OnPremises;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AppFileUploader.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            string storageType = configuration.GetSection("InfraStructure:Mode").Value.ToString().ToUpper();
            services.AddDbContext<AppManagementDbContext>(
                options => options.UseMySql(
                    configuration.GetConnectionString("MySQLDB"),
                    ServerVersion.AutoDetect(configuration.GetConnectionString("MySQLDB"))
                )
            );
            services.AddScoped(typeof(IFileContent<>), typeof(FileContentRepo<>));

            switch (storageType)
            {
                case "ONPREM":
                    services.AddTransient<IStorage, StorageAdapterOnPrem>();
                    break;
            }
            
            return services;
        }
    }
}