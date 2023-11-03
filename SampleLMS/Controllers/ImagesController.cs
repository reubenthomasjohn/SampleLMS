using Microsoft.AspNetCore.Mvc;
using SampleLMS.Dal.Interfaces;
using System.Net;

namespace SampleLMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    // Api Controllers return HTTP responses, MVC Controllers returns views, redirect etc.
    public class ImagesController : ControllerBase
    {
        private readonly IImageInterface imageRepository;

        public ImagesController(IImageInterface imageRepository)
        {
            this.imageRepository = imageRepository;
        }
        [HttpPost]
        public async Task<IActionResult> UploadAsync(IFormFile file)
        {
            // call the repository
            var imageURL = await imageRepository.UploadAsync(file);
            if (imageURL == null)
            {
                return Problem("Something went wrong!", null, (int)HttpStatusCode.InternalServerError);
            }

            return new JsonResult(new { link = imageURL });
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok("Working...");
        }
    }
}
