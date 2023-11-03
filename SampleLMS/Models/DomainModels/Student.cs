using Microsoft.AspNetCore.Identity;

namespace SampleLMS.Models.DomainModels
{
    public class StudentUser : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public ICollection<Enrollments> Enrollments { get; set; } = new List<Enrollments>();
    }
}
