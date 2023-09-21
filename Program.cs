using Microsoft.EntityFrameworkCore;
using TestTaskGFL.Database;
using TestTaskGFL.Services.IOFoldersService.ExportService;
using TestTaskGFL.Services.IOFoldersService.ImportService;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<FolderDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

builder.Services.AddScoped<IFolderExportService, FolderExportService>();
builder.Services.AddScoped<IFolderImportService, FolderImportService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Folder}/{action=Index}/{id?}");

app.Run();
