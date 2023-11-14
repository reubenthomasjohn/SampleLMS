using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SampleLMS.Dal.Interfaces;
using SampleLMS.Models.DomainModels;
using SampleLMS.Models.DTOs.Module;

namespace SampleLMS.Controllers
{
	public class ModuleController : Controller
	{
		private readonly IModuleInterface moduleRepository;
        private readonly UserManager<IdentityUser> userManager;

        public ModuleController(IModuleInterface moduleRepository, UserManager<IdentityUser> userManager)
        {
			this.moduleRepository = moduleRepository;
            this.userManager = userManager;
        }
		[HttpGet]
        public async Task<IActionResult> Edit(int moduleId)
		{
			var module = await moduleRepository.GetSingleModuleAsync(moduleId);

			//var coursesLinkedToModule = module.Courses.ToList();

			//var courseIds = new List<int>();
			//foreach (var course in coursesLinkedToModule)
			//{
			//	courseIds.Add(course.CourseId);
			//}

			var editModuleView = new EditModuleRequest
			{
				//CourseIds = courseIds,
				ModuleId = module.ModuleId,
				ModuleName = module.ModuleName,
                ContentType = module.ContentType,
				ModuleContent = module.ModuleContent
            };

			return View(editModuleView);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(EditModuleRequest editModuleRequest)
        {

			var existingModule = await moduleRepository.GetSingleModuleAsync(editModuleRequest.ModuleId);
			var coursesLinkedWithModule = existingModule.Courses.ToList();

            // map view model back to domain model
            var updatedModule = new Module
			{
				ModuleId = editModuleRequest.ModuleId,
				ModuleName = editModuleRequest.ModuleName,
				ContentType = editModuleRequest.ContentType,
				ModuleContent = editModuleRequest.ModuleContent,
				UploadedFilePaths = new List<FilePath>(),
				Courses = coursesLinkedWithModule
            };

			if (editModuleRequest.UploadedFiles is not null)
			{
                // Deserialize the JSON string to get the List<string>
                List<string> uploadedFilesList = JsonConvert.DeserializeObject<List<string>>(editModuleRequest.UploadedFiles);
                foreach (var file in uploadedFilesList)
                {
                    var filePath = new FilePath
                    {
                        Module = updatedModule,
                        Path = file,
                        ModuleId = editModuleRequest.ModuleId
                    };
                    updatedModule.UploadedFilePaths.Add(filePath);
                }
            }
           
			var updatedModuleInRepository = moduleRepository.UpdateModuleAsync(updatedModule);

			if (updatedModuleInRepository is not null)
			{
				return RedirectToAction("List", "Course");
            }
		return View();
			
		}

		[HttpGet]
		public IActionResult Add()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Add(Module module)
		{
			return View();
		}

		[HttpGet]
		public IActionResult Delete(int moduleId)
		{
			return View("Course", "List");
		}

		[HttpGet]
		public IActionResult ModuleDetailedView(int moduleId)
		{
			return View();
		}
	}
}
