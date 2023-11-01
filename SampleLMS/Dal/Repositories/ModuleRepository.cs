using SampleLMS.Dal.Interfaces;
using SampleLMS.Models.DomainModels;

namespace SampleLMS.Dal.Repositories
{
    public class ModuleRepository : IModuleInterface
    {
        public Task<Module> AddModuleAsync(Module module)
        {
            throw new NotImplementedException();
        }

        public Task<Module?> DeleteMoudleAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Module>> GetAllModulesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Module>> GetAllModulesByCategory(Category category)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Module>> GetAllModulesByCourse(Course course)
        {
            throw new NotImplementedException();
        }

        public Task<Module?> GetSingleModuleAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Module?> UpdateModuleAsync(Module module)
        {
            throw new NotImplementedException();
        }
    }
}
