using SampleLMS.Models.DomainModels;

namespace SampleLMS.Dal.Interfaces
{
    public interface ICategoryInterface
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<IEnumerable<Category>> GetAllCategoriesForCourseAsync(Course course);
        Task<Category?> GetAsync(int id);
        Task<Category> AddAsync(Category category);
        Task<Category?> UpdateAsync(Category category);

        Task<Category?> DeleteAsync(int id);
    }
}
