using AppFileUploader.Application.Contract.Persistence;
using AppFileUploader.Domain.Entities;
using System.Diagnostics;


namespace AppFileUploader.Infrastructure.Persistence
{
    public class FileContentRepo<T> : IFileContent<T> where T : FileContent
    {
        protected readonly AppManagementDbContext _context;
        private static readonly ActivitySource ActivitySource = new("DBLayer");

        public FileContentRepo( AppManagementDbContext context)
        {
            _context = context;
        }
        public async Task<T> AddAsync(T entity)
        {
            using var activity = ActivitySource.StartActivity("FileContentRepo-AddAsync");
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
            activity?.SetTag("DB.id", entity.Id);
            return entity;
        }

        public Task<IReadOnlyList<T>> GetAllFileContentAsync()
        {
            throw new NotImplementedException();
        }
    }
}