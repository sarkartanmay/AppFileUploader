using AppFileUploader.Application.Contract.Storage;
using AppFileUploader.Infrastructure.Storage.OnPremises;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFileUploader.Infrastructure.Storage.AzureBlob
{
    public class StorageAdapterAzureBlob : IStorage
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<StorageAdapterAzureBlob> _logger;

        public StorageAdapterAzureBlob(IConfiguration configuration, ILogger<StorageAdapterAzureBlob> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<bool> MoveFiles(IFormFile file)
        {
            var BlobStorageConn = _configuration.GetSection("InfraStructure:AzureBlob:BlobStorageConn").Value;
            var BlobStorageContainer = _configuration.GetSection("InfraStructure:AzureBlob:BlobStorageContainer").Value;

            if (file.Length > 0)
            {
                try
                {
                    var container = new BlobContainerClient(BlobStorageConn, BlobStorageContainer);
                    var blob = container.GetBlobClient(file.FileName);

                    var stream = file.OpenReadStream();
                    await blob.UploadAsync(stream);

                    _logger.LogInformation("File has been uploaded to Azure {url}", container.Uri.AbsoluteUri);
                    return true;
                }
                catch (Exception ex)
                {
                    _logger.LogError("There is an issue to Upload on Azure");
                    _logger.LogError(ex.Message);
                    return false;
                }
                
            }
            else
            {
                _logger.LogError("There is an issue with file to Upload on Azure");
                return false;
            }

        }
    }
}
