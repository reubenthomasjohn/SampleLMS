using Microsoft.AspNetCore.Mvc.Rendering;

namespace SampleLMS.Models.DTOs.Course
{
    public class AddCourseRequest
    {
        public string Heading { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Description { get; set; }
        public string FeaturedImageUrl { get; set; }
        public string UrlHandle { get; set; }
        public DateTime PublishedDate { get; set; }
        public string Author { get; set; }
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
