using Microsoft.EntityFrameworkCore;
using SampleLMS.Models.DomainModels;

namespace SampleLMS.Dal.Interfaces
{
    public class CategoryRepository : ICategoryInterface
    {
        private readonly CourseDbContext dbContext;

        public CategoryRepository(CourseDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Category> AddAsync(Category category)
        {
            await dbContext.Categories.AddAsync(category);
            await dbContext.SaveChangesAsync();
            return category;
        }

        public async Task<Category?> DeleteAsync(int id)
        {
            var existingTag = await dbContext.Categories.FindAsync(id);
            if (existingTag != null)
            {
                dbContext.Categories.Remove(existingTag);
                await dbContext.SaveChangesAsync();
                return existingTag;
            }
            return null;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await dbContext.Categories.ToListAsync();
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesForCourseAsync(Course course)
        {
            var courseId = course.CourseId;
            var categoriesForCourse = await dbContext.Categories
                                .Where(c => c.Courses.Any(course => course.CourseId == courseId))
                                .ToListAsync();
            return categoriesForCourse;
        }

        public async Task<Category?> GetAsync(int id)
        {
            return await dbContext.Categories.FirstOrDefaultAsync(x => x.CategoryId == id);
        }

        public async Task<Category?> UpdateAsync(Category category)
        {
            var existingTag = await dbContext.Categories.FindAsync(category.CategoryId);

            if (existingTag != null)
            {
                existingTag.Name = category.Name;

                await dbContext.SaveChangesAsync();

                return existingTag;
            }

            return null;
        }
    }
}

