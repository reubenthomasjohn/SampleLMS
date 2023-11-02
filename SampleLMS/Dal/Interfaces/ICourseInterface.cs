using SampleLMS.Models.DomainModels;

namespace SampleLMS.Dal.Interfaces
{
    public interface ICourseInterface
    {
        Task<Course> CreateCourseAsync(Course course);
        Task<Course?> UpdateCourseAsync(Course updatedCourse);
        Task<Course?> DeleteCourseAsync(int id);
        Task<Course?> GetSingleCourseAsync(int courseId);
        Task<IEnumerable<Course>> GetAllCoursesAsync();
		Task<Course?> GetByUrlHandleAsync(string urlHandle);
		//Task<Course?> GetByCourseIdAsync(int courseId);
	}
}
