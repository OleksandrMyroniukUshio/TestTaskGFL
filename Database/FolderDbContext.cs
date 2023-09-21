using Microsoft.EntityFrameworkCore;
using TestTaskGFL.Models.Folders;
using TestTaskGFL.Models;

namespace TestTaskGFL.Database
{
    public class FolderDbContext : DbContext
    {
        public DbSet<Folder> Folders { get; set; }

        public FolderDbContext(DbContextOptions<FolderDbContext> options) : base(options) { }
    }
}
