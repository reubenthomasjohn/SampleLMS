using System.ComponentModel.DataAnnotations;

namespace SampleLMS.Models.DomainModels
{
    public class Course
    {
        [Key] 
        public int CourseId { get; set; }
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required]
        public TimeSpan Duration { get; set; } = default;

        // Navigation Properties
        public ICollection<CourseCategory>? CourseCategories { get; set; }
        public ICollection<CourseModule>? CourseModules { get; set; }

    }
}
