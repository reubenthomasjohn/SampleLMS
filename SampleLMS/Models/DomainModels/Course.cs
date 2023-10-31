using System.ComponentModel.DataAnnotations;

namespace SampleLMS.Models.DomainModels
{
    public class Course
    {
        [Key] 
        public int Id { get; set; }
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required]
        public List<string> CategoriesList { get; set; } = new List<string>();
        [Required]
        public TimeSpan Duration { get; set; } = default(TimeSpan);
        
        // Navigation Properties
        public ICollection<CategoryTag>? Categories { get; set; }
        public ICollection<Module>? Modules { get; set; }

    }
}
