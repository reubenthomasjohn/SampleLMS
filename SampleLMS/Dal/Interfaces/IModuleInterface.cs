﻿using SampleLMS.Models.DomainModels;

namespace SampleLMS.Dal.Interfaces
{
    public interface IModuleInterface
    {
        Task<IEnumerable<Module>> GetAllModulesAsync();
        Task<IEnumerable<Module>> GetAllModulesByCategory(Category category);
        Task<IEnumerable<Module>> GetAllModulesForCourse(Course course);

        Task<Module?> GetSingleModuleAsync(int id);
        Task<Module> AddModuleAsync(Module module);
        Task<Module?> UpdateModuleAsync(Module module);

        Task<Module?> DeleteModuleAsync(int id);
    }
}
