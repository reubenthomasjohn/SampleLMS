using System.ComponentModel.DataAnnotations;

namespace SampleLMS.Models.DomainModels
{
    public class Course
    {
        [Key] 
        public int CourseId { get; set; }
		public string? Heading { get; set; } = string.Empty;
		[Required]
        public string Title { get; set; } = string.Empty;
        [Required]
		public string? Content { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public string? FeaturedImageUrl { get; set; } = string.Empty;
		public string? UrlHandle { get; set; } = string.Empty;
		public DateTime PublishedDate { get; set; } = default;
		public string? Author { get; set; } = string.Empty;
		[Required]
        public TimeSpan? Duration { get; set; } = default;

        // Navigation Properties
        public ICollection<Category>? Categories { get; set; }
        public ICollection<Module>? Modules { get; set; }
        public ICollection<Instructor> Instructors { get; set; } = new List<Instructor>();
        public ICollection<Enrollments> Enrollments { get; set; } = new List<Enrollments>();

    }
}
