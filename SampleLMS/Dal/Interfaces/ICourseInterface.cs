using SampleLMS.Models.DomainModels;

namespace SampleLMS.Dal.Interfaces
{
    public interface ICourseInterface
    {
        Task<Course> CreateCourse(Course course);
        Task<Course?> UpdateCourse(Course updatedCourse);
        Task<Course?> DeleteCourse(int id);
        Task<Course?> GetSingleCourse(int courseId);
        Task<IEnumerable<Course>> GetAllCourses();
    }
}
