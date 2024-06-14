using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFileUploader.Application.Contract.Storage
{
    public interface IStorage
    {
        public Task<bool> MoveFiles(IFormFile File);
    }
}
