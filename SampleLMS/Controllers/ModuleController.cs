using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
			var editModuleView = new EditModuleRequest
			{
				ModuleId = module.ModuleId,
				ModuleName = module.ModuleName,
                ContentType = module.ContentType
            };

			return View(editModuleView);
		}

		[HttpPost]
		public IActionResult Edit(EditModuleRequest editModuleRequest)
        {
				
            // map view model back to domain model
            var updatedModule = new Module
			{
				ModuleId = editModuleRequest.ModuleId,
				ModuleName = editModuleRequest.ModuleName,
				ContentType = editModuleRequest.ContentType,
			};

			var fileToBeUploaded = editModuleRequest.UploadedFiles;

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
