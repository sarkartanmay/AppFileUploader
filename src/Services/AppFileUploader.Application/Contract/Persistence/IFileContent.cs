using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppFileUploader.Domain.Entities;

namespace AppFileUploader.Application.Contract.Persistence
{
    public interface IFileContent<T> where T : FileContent
    {
        Task<IReadOnlyList<T>> GetAllFileContentAsync();
        Task<T> AddAsync(T entity);
    }
}