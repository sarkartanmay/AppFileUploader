using AppFileUploader.Application.Contract.Storage;
using AppFileUploader.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFileUploader.Infrastructure.Storage.OnPremises
{
    public class StorageAdapterOnPrem : IStorage
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<StorageAdapterOnPrem> _logger;

        public StorageAdapterOnPrem(IConfiguration configuration, ILogger<StorageAdapterOnPrem> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public bool MoveFiles(IFormFile file)
        {
            if (file.Length > 0)
            {
                using (FileStream fs = File.Create(_configuration.GetSection("InfraStructure:OnPrem:UploadPath").Value + file.FileName))
                {
                    file.CopyTo(fs);
                    fs.Flush();
                    _logger.LogInformation("File has been uploaded");
                    return true;
                }
            }
            else
            {
                _logger.LogError("There is an issue with file");
                return false;
            }
            
        }
    }
}
