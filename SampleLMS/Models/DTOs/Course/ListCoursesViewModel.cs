using System.ComponentModel.DataAnnotations;

namespace SampleLMS.Models.DTOs.Course
{
	public class ListCoursesViewModel
	{
		public int CourseId { get; set; }
		public string? Heading { get; set; }
		[Required]
		public string Description { get; set; } = string.Empty;
	}
}
