using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TestTaskGFL.Models.Folders
{
    public class Folder
    {
        public int ID { get; set; }
        public string Name { get; set; } = "Undefined";
        public int? ParentID { get; set; }

        public virtual Folder Parent { get; set; }
        public virtual ICollection<Folder> Children { get; set; } = new List<Folder>();
    }
}
