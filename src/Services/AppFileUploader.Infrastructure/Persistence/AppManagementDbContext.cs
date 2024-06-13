using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppFileUploader.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AppFileUploader.Infrastructure.Persistence
{
    public class AppManagementDbContext : DbContext
    {
        public AppManagementDbContext(DbContextOptions<AppManagementDbContext> options) : base(options) { }

        public DbSet<FileContent> FileContents { get; set; }
    }
}