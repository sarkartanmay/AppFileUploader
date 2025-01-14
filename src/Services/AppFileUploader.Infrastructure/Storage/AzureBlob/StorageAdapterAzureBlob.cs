﻿using AppCommonSettings;
using AppFileUploader.Application.Contract.Storage;
using AppFileUploader.Infrastructure.Storage.OnPremises;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFileUploader.Infrastructure.Storage.AzureBlob
{
    public class StorageAdapterAzureBlob : IStorage
    {
        private readonly ILogger<StorageAdapterAzureBlob> _logger;
        private readonly ApplicationOptions _options;

        public StorageAdapterAzureBlob(ILogger<StorageAdapterAzureBlob> logger, IOptions<ApplicationOptions> options)
        {
            _logger = logger;
            _options = options.Value;
        }

        public async Task<bool> MoveFiles(IFormFile file)
        {
            var BlobStorageConn = _options.InfraStructure.AzureBlob.BlobStorageConn;
            var BlobStorageContainer = _options.InfraStructure.AzureBlob.BlobStorageContainer;

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
