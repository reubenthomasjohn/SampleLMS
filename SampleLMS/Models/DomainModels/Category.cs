using System.ComponentModel.DataAnnotations;

namespace SampleLMS.Models.DomainModels
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        public string? Name { get; set; }
        public ICollection<CourseCategory>? CourseCategories { get; set; }
    }
}
