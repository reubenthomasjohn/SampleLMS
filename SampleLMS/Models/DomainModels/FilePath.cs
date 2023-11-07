using System.ComponentModel.DataAnnotations;

namespace SampleLMS.Models.DomainModels
{
    public class FilePath
    {
        [Key]
        public int FilePathId { get; set; }

        public string Path { get; set; }

        public int ModuleId { get; set; }
        public Module Module { get; set; }
    }
}
