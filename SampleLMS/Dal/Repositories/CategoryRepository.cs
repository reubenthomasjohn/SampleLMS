using Microsoft.EntityFrameworkCore;
using SampleLMS.Data;
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
            var existingCategory = await dbContext.Categories.FindAsync(id);
            if (existingCategory != null)
            {
                dbContext.Categories.Remove(existingCategory);
                await dbContext.SaveChangesAsync();
                return existingCategory;
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
            var existingCategory = await dbContext.Categories.FindAsync(category.CategoryId);

            if (existingCategory != null)
            {
                existingCategory.Name = category.Name;
                await dbContext.SaveChangesAsync();
                return existingCategory;
            }

            return null;
        }
    }
}

