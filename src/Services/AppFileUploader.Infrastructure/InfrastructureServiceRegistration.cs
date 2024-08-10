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
using AppCommonSettings;

namespace AppFileUploader.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, ApplicationOptions applicationOptions)
        {
            string storageType = applicationOptions.InfraStructure.Mode.ToUpper();
            string dbConn = $"Server={applicationOptions.MySqlDb.Server};" +
                $"User ID={applicationOptions.MySqlDb.User};" +
                $"Password={applicationOptions.MySqlDb.Password};" +
                $"Database={applicationOptions.MySqlDb.Database};" +
                $"SSL Mode={applicationOptions.MySqlDb.SSLMode}" ;
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