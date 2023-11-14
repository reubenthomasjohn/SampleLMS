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
        public async Task<IActionResult> UploadFile(List<IFormFile> files)
        {
            var result = await _storageService.UploadFileAsync(files);

            //return Ok(result.FileURLs);
            return new JsonResult(new { links = result.FileURLs });
        }


        [HttpGet]
        public IActionResult Index()
        {
            return Ok("Working...");
        }
    }
}
