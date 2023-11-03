using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace SampleLMS.Models.DTOs.Course
{
	public class EditCourseRequest
	{
		public int CourseId { get; set; }
		public string? Heading { get; set; } = string.Empty;
		public string Title { get; set; } = string.Empty;
		public string? Content { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public string? FeaturedImageUrl { get; set; } = string.Empty;
		public string? UrlHandle { get; set; } = string.Empty;
		public DateTime PublishedDate { get; set; } = default;
		public string? Author { get; set; } = string.Empty;
		public TimeSpan? Duration { get; set; } = default;

        // Display categories
        public IEnumerable<SelectListItem> Categories { get; set; }
        // Collect category
        public string[] SelectedCategories { get; set; } = Array.Empty<string>();

        // Display modules
        public IEnumerable<SelectListItem> Modules { get; set; }
        // Collect modules
        public string[] SelectedModules { get; set; } = Array.Empty<string>();
    }
}
