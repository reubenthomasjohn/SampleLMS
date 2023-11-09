using Microsoft.AspNetCore.Mvc;
using SampleLMS.Services;

namespace SampleLMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadsController : ControllerBase
    {
        private readonly IStorageServiceInterface _storageService;

        public FileUploadsController(
                IStorageServiceInterface storageService)
        {
            _storageService = storageService;
        }

        [HttpPost(Name = "UploadFile")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            var result = await _storageService.UploadFileAsync(file);

            return Ok(result);
        }


        [HttpGet]
        public IActionResult Index()
        {
            return Ok("Working...");
        }
    }
}
