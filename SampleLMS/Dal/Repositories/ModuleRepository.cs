using CloudinaryDotNet;
using Microsoft.EntityFrameworkCore;
using SampleLMS.Dal.Interfaces;
using SampleLMS.Data;
using SampleLMS.Models.DomainModels;

namespace SampleLMS.Dal.Repositories
{
    public class ModuleRepository : IModuleInterface
    {
        private readonly CourseDbContext dbContext;

        public ModuleRepository(CourseDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Module> AddModuleAsync(Module module)
        {
            await dbContext.Modules.AddAsync(module);
            await dbContext.SaveChangesAsync();
            return module;
        }

        public async Task<Module?> DeleteModuleAsync(int id)
        {
            var existingModule = await dbContext.Modules.FindAsync(id);
            if (existingModule != null)
            {
                dbContext.Modules.Remove(existingModule);
                await dbContext.SaveChangesAsync();
                return existingModule;
            }
            return null;
        }

        public async Task<IEnumerable<Module>> GetAllModulesAsync()
        {
            return await dbContext.Modules.ToListAsync();
        }

        public async Task<IEnumerable<Module>> GetAllModulesByCategory(Category category)
        {
            throw new NotImplementedException();
            //var categoryId = category.CategoryId;
            //var modulesForCategory = await dbContext.Modules
            //                    .Where(c => c.Course.Any(category => category.CategoryId == categoryId))
            //                    .ToListAsync();
            //return modulesForCategory;
        }

        public async Task<IEnumerable<Module>> GetAllModulesForCourse(Course course)
        {
            var courseId = course.CourseId;
            var modulesForCourse = await dbContext.Modules
                                .Where(c => c.Courses.Any(course => course.CourseId == courseId))
                                .ToListAsync();
            return modulesForCourse;
        }

        public async Task<Module?> GetSingleModuleAsync(int id)
        {
            return await dbContext.Modules
                .Include(m => m.Courses)
                .Include(fp => fp.UploadedFilePaths)
                .FirstOrDefaultAsync(x => x.ModuleId == id);
        }

        public async Task<Module?> UpdateModuleAsync(Module module)
        {
            var existingModule = await dbContext.Modules.FindAsync(module.ModuleId);

            if (existingModule != null)
            {
                existingModule.ModuleId = module.ModuleId;
                existingModule.ModuleName = module.ModuleName;
                existingModule.ModuleContent = module.ModuleContent;
                existingModule.ContentType = module.ContentType;
                existingModule.UploadedFilePaths = module.UploadedFilePaths;
                existingModule.Courses = module.Courses;

                await dbContext.SaveChangesAsync();
                return existingModule;
            }

            return null;
        }
    }
}
