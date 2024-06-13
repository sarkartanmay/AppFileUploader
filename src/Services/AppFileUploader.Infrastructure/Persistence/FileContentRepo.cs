using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppFileUploader.Application.Contract.Persistence;
using AppFileUploader.Domain.Entities;

namespace AppFileUploader.Infrastructure.Persistence
{
    public class FileContentRepo<T> : IFileContent<T> where T : FileContent
    {
        protected readonly AppManagementDbContext _context;
        public FileContentRepo(AppManagementDbContext context)
        {
            _context = context;
        }
        public async Task<T> AddAsync(T entity)
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public Task<IReadOnlyList<T>> GetAllFileContentAsync()
        {
            throw new NotImplementedException();
        }
    }
}