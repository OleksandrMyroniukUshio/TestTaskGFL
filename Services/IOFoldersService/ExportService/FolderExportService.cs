using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text;
using TestTaskGFL.Database;
using TestTaskGFL.Models.Folders;

namespace TestTaskGFL.Services.IOFoldersService.ExportService
{
    public class FolderExportService : IFolderExportService
    {
        private readonly FolderDbContext _dbcontext;

        public FolderExportService(FolderDbContext dbContext)
        {
            _dbcontext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }


        public string ExportToFile()
        {
            var allFolders = _dbcontext.Folders.ToList();

            var topLevelFolders = allFolders.Where(f => f.ParentID == null).ToList();

            var folderDtos = topLevelFolders.Select(f => ConvertToDto(f, allFolders)).ToList();

            return JsonConvert.SerializeObject(folderDtos);
        }

        private FolderDto ConvertToDto(Folder folder, List<Folder> allFolders)
        {
            return new FolderDto
            {
                Name = folder.Name,
                Children = allFolders
                    .Where(f => f.ParentID == folder.ID)
                    .Select(f => ConvertToDto(f, allFolders))
                    .ToList()
            };
        }
    }
}
