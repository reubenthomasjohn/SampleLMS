using Microsoft.EntityFrameworkCore;
using SampleLMS.Models.DomainModels;

namespace SampleLMS.Data
{
    public class CourseDbContext : DbContext
    {
        public CourseDbContext(DbContextOptions<CourseDbContext> options)
            : base(options) { }

        public DbSet<Course> Courses => Set<Course>();
        public DbSet<Module> Modules => Set<Module>();
        public DbSet<Category> Categories => Set<Category>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Defining the many to many relationship between course and category
            modelBuilder.Entity<Course>()
                .HasMany(x => x.Categories)
                .WithMany(y => y.Courses)
                .UsingEntity<Dictionary<string, object>>(
                "CourseCategory",
                r => r.HasOne<Category>().WithMany().HasForeignKey("CategoryId"),
                l => l.HasOne<Course>().WithMany().HasForeignKey("CourseId"),
                je =>
                {
                    je.HasKey("CourseId", "CategoryId");
                    je.HasData(
                        new { CourseId = 1, CategoryId = 1 },
                        new { CourseId = 1, CategoryId = 2 },
                        new { CourseId = 2, CategoryId = 1 },
                        new { CourseId = 2, CategoryId = 2 },
                        new { CourseId = 2, CategoryId = 3 }
                        );
                });

            modelBuilder.Entity<Course>()
                .HasMany(x => x.Modules)
                .WithMany(y => y.Courses)
                .UsingEntity<Dictionary<string, object>>(
                "CourseModule",
                r => r.HasOne<Module>().WithMany().HasForeignKey("ModuleId"),
                l => l.HasOne<Course>().WithMany().HasForeignKey("CourseId"),
                je =>
                {
                    je.HasKey("CourseId", "ModuleId");
                    je.HasData(
                        new { CourseId = 1, ModuleId = 1 },
                        new { CourseId = 1, ModuleId = 2 },
                        new { CourseId = 2, ModuleId = 1 },
                        new { CourseId = 2, ModuleId = 2 },
                        new { CourseId = 2, ModuleId = 3 }
                        );
                });

            // seeding the DB with sample data

            Category categoryCloud = new Category
            {
                CategoryId = 1,
                Name = "Cloud",
            };

            Category categoryDevOps = new Category
            {
                CategoryId = 2,
                Name = "DevOps"
            };

            Category categoryContainerization = new Category
            {
                CategoryId = 3,
                Name = "Containerization"
            };

            Module moduleContainers = new Module
            {
                ModuleId = 1,
                ModuleName = "Containers",
            };

            Module moduleK8s = new Module
            {
                ModuleId = 2,
                ModuleName = "K8s Introductions",
            };

            Module moduleDocker = new Module
            {
                ModuleId = 3,
                ModuleName = "Basic Docker Commands",
            };

            Course courseDocker = new Course
            {
                CourseId = 1,
                Heading = "Docker",
                Title = "Docker101",
                Content = "aaaaaaaaaaaabbbbbbbccccccccc",
                Description = "Docker is a containerization tool, used by all kinds of engineers.",
                Duration = TimeSpan.FromHours(2.5),
                //Categories = new List<Category>
                //{
                //    categoryTagCloud,
                //    categoryTagDevOps
                //},

                //Modules = new List<Module>
                //{
                //    moduleContainers,
                //    moduleDocker,
                //}
            };

            Course courseK8s = new Course
            {
                CourseId = 2,
                Heading = "Kubernetes",
                Title = "Kubernetes101",
                Content = "aaaaaaaaaaaabbbbbbbccccccccc",
                Description = "Kubernetes is a container orchestration tool",
                Duration = TimeSpan.FromHours(5.5),
                //Categories = new List<Category>
                //{
                //    categoryTagCloud,
                //    categoryTagDevOps,
                //    categoryTagContainerization
                //},

                //Modules = new List<Module>
                //{
                //    moduleContainers,
                //    moduleDocker,
                //    moduleK8s
                //}
                //Categories = new List<CategoryTag> { categoryTagDevOps, categoryTagContainerization, categoryTagCloud },
                //Modules = new List<Module> { moduleK8s }
            };
            modelBuilder.Entity<Course>().HasData(
            courseDocker, courseK8s);

            modelBuilder.Entity<Module>().HasData(
                moduleContainers, moduleDocker, moduleK8s);

            modelBuilder.Entity<Category>().HasData(
                categoryContainerization, categoryCloud, categoryDevOps);


            base.OnModelCreating(modelBuilder);
        }

    }
}
