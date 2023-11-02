using SampleLMS.Models.DomainModels;
using System.ComponentModel.DataAnnotations;

namespace SampleLMS.Models.DTOs.Course
{
	public class CourseDetailsViewModel
	{
		public string? Heading { get; set; }
		public string Title { get; set; } = string.Empty;
		public string? Content { get; set; }
		public string Description { get; set; } = string.Empty;
		public string? FeaturedImageUrl { get; set; }
		public string? UrlHandle { get; set; }
		public DateTime PublishedDate { get; set; } = default;
		public string? Author { get; set; }
		[Required]
		public TimeSpan? Duration { get; set; } = default;

		// Navigation Properties
		public ICollection<DomainModels.Category>? Categories { get; set; }
		public ICollection<Module>? Modules { get; set; }
	}
}
