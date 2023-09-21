using TestTaskGFL.Models.Folders;

namespace TestTaskGFL.Services.IOFoldersService.ImportService
{
    public interface IFolderImportService
    {
        Folder? GetFolder(int? id);
        Task<IEnumerable<Folder>?> ImportFromFileAsync(IFormFile file);
    }
}
