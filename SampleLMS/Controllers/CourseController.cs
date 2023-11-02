using Microsoft.AspNetCore.Mvc;
using SampleLMS.Dal.Interfaces;
using SampleLMS.Models.DTOs.Course;

namespace SampleLMS.Controllers
{
    public class CourseController : Controller
    {
		private readonly ICategoryInterface categoryRepository;
		private readonly ICourseInterface courseRepository;

		public CourseController(ICategoryInterface categoryRepository,
								ICourseInterface courseRepository)
        {
			this.categoryRepository = categoryRepository;
			this.courseRepository = courseRepository;
		}

		[HttpGet]
		public async Task<IActionResult> List()
		{
			var courses = await courseRepository.GetAllCoursesAsync();
			var coursesInListView = new List<ListCoursesViewModel>();
			foreach (var course in courses)
			{
				var courseInListView = new ListCoursesViewModel
				{
					CourseId = course.CourseId,
					Heading = course.Heading,
					Description = course.Description
				};
				coursesInListView.Add(courseInListView);
			}
			return View(coursesInListView);
		}

		[HttpGet]
		public async Task<IActionResult> CourseDetailedView(int courseId)
		{
			var course = await courseRepository.GetSingleCourseAsync(courseId);

			if (course != null)
			{
				var courseDetailsView = new CourseDetailsViewModel
				{
					Heading = course.Heading,
					Title = course.Title,
					Content = course.Content,
					Description = course.Description,
					FeaturedImageUrl = course.FeaturedImageUrl,
					UrlHandle = course.UrlHandle,
					PublishedDate = course.PublishedDate,
					Author = course.Author,
					Duration = course.Duration,
					Categories = course.Categories,
					Modules = course.Modules
				};
				return View(courseDetailsView);
			}
			return RedirectToAction("List");
		}
	}
}
