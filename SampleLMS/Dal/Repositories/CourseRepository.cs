using Microsoft.EntityFrameworkCore;
using SampleLMS.Dal.Interfaces;
using SampleLMS.Data;
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
        public async Task<Course> CreateCourseAsync(Course course)
        {
            await dbContext.Courses.AddAsync(course);
            await dbContext.SaveChangesAsync();
            return course;
        }

		public async Task<Course?> DeleteCourseAsync(int id)
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

        public async Task<IEnumerable<Course>> GetAllCoursesAsync()
        {
            List<Course> courses = await dbContext.Courses
            .Include(c => c.Categories) // Include the Category entity
            .Include(m => m.Modules)
            .ToListAsync();

            return courses;
        }

		public async Task<Course?> GetByUrlHandleAsync(string urlHandle)
		{
			return await dbContext.Courses.Include(x => x.Categories)
                .FirstOrDefaultAsync(x => x.UrlHandle == urlHandle);
		}

		public async Task<Course?> GetSingleCourseAsync(int courseId)
        {
            return await dbContext.Courses
                .Include(c => c.Categories)
                .Include(m => m.Modules)
                .FirstOrDefaultAsync(c => c.CourseId == courseId);
        }
		public async Task<Course?> UpdateCourseAsync(Course updatedCourse)
        {
            var existingCourse = await dbContext.Courses
                .FirstOrDefaultAsync(x => x.CourseId == updatedCourse.CourseId);

            if (existingCourse is not null)
            {
                existingCourse.CourseId = updatedCourse.CourseId;
                existingCourse.Heading = updatedCourse.Heading;
                existingCourse.Title = updatedCourse.Title;
                existingCourse.Content = updatedCourse.Content;
                existingCourse.Description = updatedCourse.Description;
                existingCourse.FeaturedImageUrl = updatedCourse.FeaturedImageUrl;
                existingCourse.UrlHandle = updatedCourse.UrlHandle;
                existingCourse.PublishedDate = updatedCourse.PublishedDate;
                existingCourse.Author = updatedCourse.Author;
                existingCourse.Duration = updatedCourse.Duration;
                existingCourse.Categories = updatedCourse.Categories;
                existingCourse.Modules = updatedCourse.Modules; 

                await dbContext.SaveChangesAsync();
                return existingCourse;
            }
            return null;
        }

	}
}
