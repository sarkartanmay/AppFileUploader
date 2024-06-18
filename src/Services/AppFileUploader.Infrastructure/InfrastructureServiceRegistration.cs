using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AppFileUploader.Application.Contract.Persistence;
using AppFileUploader.Application.Contract.Storage;
using AppFileUploader.Infrastructure.Persistence;
using AppFileUploader.Infrastructure.Storage.AzureBlob;
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
            string dbConn = $"Server={configuration.GetConnectionString("MySQLDB:Server")};" +
                $"User ID={configuration.GetConnectionString("MySQLDB:User")};" +
                $"Password={configuration.GetConnectionString("MySQLDB:Password")};" +
                $"Database={configuration.GetConnectionString("MySQLDB:Database")};" +
                $"SSL Mode=None" ;
            services.AddDbContext<AppManagementDbContext>(
                options => options.UseMySql(dbConn,
                    ServerVersion.AutoDetect(dbConn)
                )
            );
            services.AddScoped(typeof(IFileContent<>), typeof(FileContentRepo<>));

            switch (storageType)
            {
                case "ONPREM":
                    services.AddTransient<IStorage, StorageAdapterOnPrem>();
                    break;
                case "AZUREBLOB":
                    services.AddTransient<IStorage, StorageAdapterAzureBlob>();
                    break;
            }
            
            return services;
        }
    }
}