using Microsoft.EntityFrameworkCore;
using SampleLMS.Models.DomainModels;

namespace SampleLMS
{
    public class CourseDbContext : DbContext
    {
        public CourseDbContext(DbContextOptions<CourseDbContext> options) 
            : base(options) { }

        public DbSet<Course> Courses => Set<Course>();
        public DbSet<Module> Modules => Set<Module>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define the one-to-many relationship between Course and Module
            modelBuilder.Entity<Module>()
                .HasOne(m => m.Course)
                .WithMany(c => c.Modules)
                .HasForeignKey(m => m.CourseId);

            modelBuilder.Entity<Course>().HasData(
                new Course
                {
                    Title = "Docker",
                    Description = "Docker is a containerization tool",
                    Category = new List<string> { "Cloud", "DevOps", "Containerization" }
                },
                new Course
                {
                    Title = "Kubernetes",
                    Description = "Kubernetes is a container orchestration tool",
                    Category = new List<string> { "Cloud", "DevOps", "Containerization", "Cloud Native" }
                });

            modelBuilder.Entity<Module>().HasData(
                new Module
                {
                    ModuleName = "Containers",
                    CourseId = 1
                },

                new Module
                {
                    ModuleName = "Basic Docker Commands",
                    CourseId = 1
                },
                new Module
                {
                    ModuleName = "K8s Introductions",
                    CourseId = 2
                });

            base.OnModelCreating(modelBuilder);
        }

    }
}
