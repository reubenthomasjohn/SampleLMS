namespace SampleLMS.Models.DomainModels
{
    public class CategoryTag
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public ICollection<Course>? Courses { get; set; }
    }
}
