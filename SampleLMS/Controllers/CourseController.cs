using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SampleLMS.Dal.Interfaces;
using SampleLMS.Models.DomainModels;
using SampleLMS.Models.DTOs.Category;
using SampleLMS.Models.DTOs.Course;

namespace SampleLMS.Controllers
{
    public class CourseController : Controller
    {
		private readonly ICategoryInterface categoryRepository;
		private readonly ICourseInterface courseRepository;
        private readonly IModuleInterface moduleRepository;

        public CourseController(ICategoryInterface categoryRepository,
								ICourseInterface courseRepository,
								IModuleInterface moduleRepository)
        {
			this.categoryRepository = categoryRepository;
			this.courseRepository = courseRepository;
            this.moduleRepository = moduleRepository;
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

		[HttpGet]
		public async Task<IActionResult> Edit(int courseId)
		{
			var course = await courseRepository.GetSingleCourseAsync(courseId); 
			var categoryDomainModel = await categoryRepository.GetAllAsync();

            var moduleDomainModel = await moduleRepository.GetAllModulesAsync();

            if (course != null)
			{
				var editCourseRequest = new EditCourseRequest
				{
					CourseId = course.CourseId,
					//DisplayName = category.DisplayName,
					Heading = course.Heading,
					Title = course.Title,
					Content = course.Content,
					Description = course.Description,
					FeaturedImageUrl = course.FeaturedImageUrl,
					UrlHandle = course.UrlHandle,
					PublishedDate = course.PublishedDate,
					Author = course.Author,
					Duration = course.Duration,
                    Categories = categoryDomainModel.Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.CategoryId.ToString(),
                    }),
                    SelectedCategories = course.Categories.Select(x => x.CategoryId.ToString()).ToArray(),


                    Modules = moduleDomainModel.Select(x => new SelectListItem
                    {
                        Text = x.ModuleName,
                        Value = x.ModuleId.ToString(),
                    }),
                    SelectedModules = course.Modules.Select(x => x.ModuleId.ToString()).ToArray()
                };
				return View(editCourseRequest);
			}
			return View(null);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(EditCourseRequest editCourseRequest)
		{

            // map view model back to domain model
            var updatedCourse = new Course
			{
				CourseId = editCourseRequest.CourseId,
				Heading = editCourseRequest.Heading,
				Title = editCourseRequest.Title,
				Content = editCourseRequest.Content,
				Description = editCourseRequest.Description,
				FeaturedImageUrl=editCourseRequest.FeaturedImageUrl,
				UrlHandle = editCourseRequest.UrlHandle,
				PublishedDate=editCourseRequest.PublishedDate,
				Author = editCourseRequest.Author,
				Duration = editCourseRequest.Duration,
			};

            // Map categories into Domain model
            var selectedCategories = new List<Category>();
            foreach (var selectedCategory in editCourseRequest.SelectedCategories)
            {
                if (int.TryParse(selectedCategory, out var categoryId))
                {
                    var foundCategory = await categoryRepository.GetAsync(categoryId);
					
                    if (foundCategory != null)
                    {
                        selectedCategories.Add(foundCategory);
                        //                  var coursesBelongingToCategory = foundCategory.Courses.ToList();
                        //foreach (var course in coursesBelongingToCategory)
                        //{
                        //	if (new { course.CourseId, categoryId } == )
                        //}
                    }                   
                }
            }
            updatedCourse.Categories = selectedCategories;

            // Map modules into Domain model
            var selectedModules = new List<Module>();
            foreach (var selectedModule in editCourseRequest.SelectedModules)
            {
                if (int.TryParse(selectedModule, out var moduleId))
                {
                    var foundModule = await moduleRepository.GetSingleModuleAsync(moduleId);
                    if (foundModule != null)
                    {
                        selectedModules.Add(foundModule);
                    }
                }
            }
            updatedCourse.Modules = selectedModules;

            var updatedCourseInRepository = await courseRepository.UpdateCourseAsync(updatedCourse);
			if (updatedCourseInRepository != null)
			{
				// show success notif
				return RedirectToAction("List");
			}
			else
			{
				// show error notif
			}
			return RedirectToAction("Edit", new { id = editCourseRequest.CourseId });
		}
	}
}
