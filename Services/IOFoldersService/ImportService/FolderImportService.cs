﻿using Microsoft.EntityFrameworkCore;
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
            return id.HasValue ?
                _dbcontext.Folders.Include(d => d.Children).Include(d => d.Parent).FirstOrDefault(d => d.ID == id) :
                _dbcontext.Folders.Include(d => d.Children).Include(d => d.Parent).FirstOrDefault(d => d.ParentID == null);
        }
        public async Task<IEnumerable<Folder>?> ImportFromFileAsync(IFormFile file) 
        {
            using var reader = new StreamReader(file.OpenReadStream());
            var content = await reader.ReadToEndAsync();
            var folders = JsonConvert.DeserializeObject<List<Folder>>(content);

            if (folders != null && folders.All(f => !string.IsNullOrEmpty(f.Name)))
            {
                await _dbcontext.Folders.AddRangeAsync(folders);
                await _dbcontext.SaveChangesAsync();
            }

            return folders;
        }
    }
}