using Microsoft.EntityFrameworkCore;
using SampleLMS.Dal.Interfaces;
using SampleLMS.Models.DomainModels;

namespace SampleLMS.Dal.Repositories
{
    public class CourseRepository : ICourseInterface
    {
        private readonly CourseDbContext dbContext;

        public CourseRepository(CourseDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Course> CreateCourse(Course course)
        {
            await dbContext.Courses.AddAsync(course);
            await dbContext.SaveChangesAsync();
            return course;
        }

        public async Task<Course?> DeleteCourse(int id)
        {
            var existingCourse = await dbContext.Courses.FindAsync(id);

            if (existingCourse != null)
            {
                dbContext.Courses.Remove(existingCourse);
                await dbContext.SaveChangesAsync();
                return existingCourse;
            }
            
            return null;
        }

        public async Task<IEnumerable<Course>> GetAllCourses()
        {
            List<Course> courses = await dbContext.Courses
            .Include(c => c.Categories) // Include the Category entity
            .ToListAsync();

            return courses;
        }

        public async Task<Course?> GetSingleCourse(int courseId)
        {
            return await dbContext.Courses.Include(c => c.Categories)
                .FirstOrDefaultAsync(c => c.CourseId == courseId);
        }

        public async Task<Course?> UpdateCourse(Course updatedCourse)
        {
            var existingCourse = await dbContext.Courses
                .FirstOrDefaultAsync(x => x.CourseId == updatedCourse.CourseId);

            if (existingCourse is not null)
            {
                existingCourse.CourseId = updatedCourse.CourseId;
                existingCourse.Title = updatedCourse.Title;
                existingCourse.Description = updatedCourse.Description;
                existingCourse.Duration = updatedCourse.Duration;

                await dbContext.SaveChangesAsync();
                return existingCourse;
            }
            return null;
        }
    }
}
