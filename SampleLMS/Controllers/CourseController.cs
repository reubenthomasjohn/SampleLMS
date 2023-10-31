using Microsoft.AspNetCore.Mvc;

namespace SampleLMS.Controllers
{
    public class CourseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
