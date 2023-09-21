namespace TestTaskGFL.Models.Folders
{
    public class FolderDto
    {
        public string Name { get; set; } = "Undefined";
        public List<FolderDto> Children { get; set; } = new List<FolderDto>();
    }

}
