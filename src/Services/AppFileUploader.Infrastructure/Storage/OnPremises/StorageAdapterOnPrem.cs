using AppCommonSettings;
using AppFileUploader.Application.Contract.Storage;
using AppFileUploader.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFileUploader.Infrastructure.Storage.OnPremises
{
    public class StorageAdapterOnPrem : IStorage
    {
        private readonly ILogger<StorageAdapterOnPrem> _logger;
        private readonly ApplicationOptions _options;


        public StorageAdapterOnPrem( ILogger<StorageAdapterOnPrem> logger , IOptions<ApplicationOptions> options)
        {
            _logger = logger;
            _options = options.Value;
        }

        

        public async Task<bool> MoveFiles(IFormFile file)
        {
            if (file.Length > 0)
            {
                FileStream fs = File.Create(_options.InfraStructure.OnPrem.UploadPath + file.FileName);
                await file.CopyToAsync(fs);
                fs.Flush();
                _logger.LogInformation("File has been uploaded");
                return true;
            }
            else
            {
                _logger.LogError("There is an issue with file"); 
                return false;
            }
        }        
    }
}
