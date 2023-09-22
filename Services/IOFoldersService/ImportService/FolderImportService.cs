using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using TestTaskGFL.Database;
using TestTaskGFL.Models.Folders;

namespace TestTaskGFL.Services.IOFoldersService.ImportService
{
    public class FolderImportService : IFolderImportService
    {
        private readonly FolderDbContext _dbcontext;

        public FolderImportService(FolderDbContext dbContext)
        {
            _dbcontext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public Folder? GetFolder(int? id)
        {
            try
            {
                return id.HasValue ?
                    _dbcontext.Folders.Include(d => d.Children).Include(d => d.Parent).FirstOrDefault(d => d.ID == id) :
                    _dbcontext.Folders.Include(d => d.Children).Include(d => d.Parent).FirstOrDefault(d => d.ParentID == null);
            }
            catch (SqlException)
            {
                return null;
            }
        }
        public async Task<IEnumerable<Folder>?> ImportFromFileAsync(IFormFile file)
        {
            using var reader = new StreamReader(file.OpenReadStream());
            var content = await reader.ReadToEndAsync();
            List<Folder> folders;

            try
            {
                folders = JsonConvert.DeserializeObject<List<Folder>>(content);
            }
            catch (JsonException)
            {
                return null;
            }

            if (folders != null && folders.All(f => !string.IsNullOrEmpty(f.Name)))
            {
                _dbcontext.Folders.RemoveRange(_dbcontext.Folders);
                await _dbcontext.Folders.AddRangeAsync(folders);
                await _dbcontext.SaveChangesAsync();
                return folders;
            }

            return null;
        }


    }
}