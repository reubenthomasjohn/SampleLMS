using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using SampleLMS.Models.DomainModels;

namespace SampleLMS
{
    public class CourseDbContext : DbContext
    {
        public CourseDbContext(DbContextOptions<CourseDbContext> options) 
            : base(options) { }

        public DbSet<Course> Courses => Set<Course>();
        public DbSet<Module> Modules => Set<Module>();
        public DbSet<Category> CategoryTags => Set<Category>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Defining the many to many relationship between course and category

            modelBuilder.Entity<CourseCategory>()
                .HasKey(bc => new {bc.CourseId, bc.CategoryId});

            modelBuilder.Entity<CourseCategory>()
                .HasOne(bc => bc.Course)
                .WithMany(b => b.CourseCategories)
                .HasForeignKey(bc => bc.CourseId);

            modelBuilder.Entity<CourseCategory>()
                .HasOne(bc => bc.Category)
                .WithMany(b => b.CourseCategories)
                .HasForeignKey(bc => bc.CategoryId);

            // defining the many-many relationship between course and module

            modelBuilder.Entity<CourseModule>()
                .HasKey(bc => new { bc.CourseId, bc.ModuleId });

            modelBuilder.Entity<CourseModule>()
                .HasOne(bc => bc.Course)
                .WithMany(b => b.CourseModules)
                .HasForeignKey(bc => bc.CourseId);

            modelBuilder.Entity<CourseModule>()
                .HasOne(bc => bc.Module)
                .WithMany(b => b.CourseModules)
                .HasForeignKey(bc => bc.ModuleId);

            //// Define the one-to-many relationship between Course and Module
            //modelBuilder.Entity<Module>()
            //    .HasMany(m => m.Courses)
            //    .WithMany(c => c.Modules)
            //    .HasForeignKey(m => m.CourseId);

            // seeding the DB with sample data

            Category categoryTagCloud = new Category
            {
                CategoryId = 1,
                Name = "Cloud",
            };

            Category categoryTagDevOps = new Category
            {
                CategoryId = 2,
                Name = "DevOps"
            };

            Category categoryTagContainerization = new Category
            {
                CategoryId = 3,
                Name = "Containerization"
            };

            Module moduleContainers = new Module
            {
                ModuleName = "Containers",
            };

            Module moduleK8s = new Module
            {
                ModuleName = "K8s Introductions",
            };

            Module moduleDocker = new Module
            {
                ModuleName = "Basic Docker Commands",
            };

            Course courseDocker = new Course
            {
                CourseId = 1,
                Title = "Docker",
                Description = "Docker is a containerization tool",
                Duration = TimeSpan.FromHours(2.5),
                CourseCategories = new List<CourseCategory>
                {
                    new CourseCategory {Category = categoryTagCloud},
                    new CourseCategory { Category = categoryTagDevOps}
                },

                CourseModules = new List<CourseModule>
                {
                    new CourseModule {Module = moduleContainers},
                    new CourseModule {Module = moduleDocker},
                }

                //WCategories = new List<CategoryTag> { categoryTagDevOps, categoryTagContainerization },
                //Modules = new List<Module> { moduleContainers, moduleDocker }
            };

            Course courseK8s = new Course
            {
                CourseId = 2,
                Title = "Kubernetes",
                Description = "Kubernetes is a container orchestration tool",
                Duration = TimeSpan.FromHours(5.5),
                CourseCategories = new List<CourseCategory>
                {
                    new CourseCategory {Category = categoryTagCloud},
                    new CourseCategory {Category = categoryTagDevOps},
                    new CourseCategory {Category = categoryTagContainerization}
                },

                CourseModules = new List<CourseModule>
                {
                    new CourseModule {Module = moduleContainers},
                    new CourseModule {Module = moduleDocker},
                    new CourseModule {Module = moduleK8s}
                }
                //Categories = new List<CategoryTag> { categoryTagDevOps, categoryTagContainerization, categoryTagCloud },
                //Modules = new List<Module> { moduleK8s }
            };

            modelBuilder.Entity<Course>().HasData(
                courseDocker, courseK8s);

            //modelBuilder.Entity<Module>().HasData(
            //    moduleContainers, moduleDocker, moduleK8s);


            //modelBuilder.Entity<Category>().HasData(
            //    categoryTagContainerization, categoryTagCloud, categoryTagDevOps);

            base.OnModelCreating(modelBuilder);
        }

    }
}
