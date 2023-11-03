using Microsoft.AspNetCore.Identity;

namespace SampleLMS.Models.DomainModels
{
    public class Instructor : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public ICollection<Course> CreatedCourses { get; set; } = new List<Course>();
    }
}
