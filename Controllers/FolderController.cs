using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TestTaskGFL.Database;
using TestTaskGFL.Models.Folders;
using TestTaskGFL.Services.IOFoldersService.ExportService;
using TestTaskGFL.Services.IOFoldersService.ImportService;

namespace TestTaskGFL.Controllers;

public class FolderController : Controller
{
    private readonly IFolderExportService _folderExportService;
    private readonly IFolderImportService _folderImportService;

    public FolderController(IFolderExportService folderExportService, IFolderImportService folderImportService)
    {
        _folderExportService = folderExportService;
        _folderImportService = folderImportService;
    }
    public IActionResult Index(int? id)
    {
        var folder = _folderImportService.GetFolder(id);
        return View(folder);
    }
    public IActionResult ImportFolder()
    {
        return View();
    }


    [HttpGet]
    public IActionResult ExportToFile()
    {
        var content = _folderExportService.ExportToFile();
        return File(Encoding.UTF8.GetBytes(content), "application/json", "export.json");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ImportFromFile(IFormFile file)
    {
        await _folderImportService.ImportFromFileAsync(file);
        return RedirectToAction("Index");
    }



}
