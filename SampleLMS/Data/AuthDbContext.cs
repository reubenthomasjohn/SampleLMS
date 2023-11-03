using Microsoft.EntityFrameworkCore;
using SampleLMS.Models.DomainModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace SampleLMS.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Seed Roles (User, Admin, SuperAdmin)

            var adminRoleId = "b21424da-6e03-4629-b7ad-c1b7eba39106";
            var instructorRoleId = "3d101212 - 2d2f - 4fa4 - bf24 - 19d52df451bb";
            var studentRoleId = "56c74461-a068-488e-99a5-2dab66c940c8";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "Admin",
                    Id = instructorRoleId,
                    ConcurrencyStamp = instructorRoleId
                },
                new IdentityRole
                {
                    Name = "Instructor",
                    NormalizedName = "Instructor",
                    Id = instructorRoleId,
                    ConcurrencyStamp = instructorRoleId
                },
                new IdentityRole
                {
                    Name = "Student",
                    NormalizedName = "Student",
                    Id = studentRoleId,
                    ConcurrencyStamp = studentRoleId
                },
            };

            builder.Entity<IdentityRole>().HasData(roles);

            // Seed SuperAdminUser
            var adminUserId = "a7822acd-9f85-4aca-8183-5d00ebcf7b34";
            var adminUser = new Instructor
            {
                FirstName = "Reuben",
                LastName = "Thomas",
                UserName = "superadmin@bloggie.com",
                Email = "superadmin@bloggie.com",
                NormalizedEmail = "superadmin@bloggie.com".ToUpper(),
                NormalizedUserName = "superadmin@bloggie.com".ToUpper(),
                Id = adminUserId
            };

            adminUser.PasswordHash = new PasswordHasher<IdentityUser>()
                .HashPassword(adminUser, "Superadmin@123");

            builder.Entity<IdentityUser>().HasData(adminUser);

            // Add All roles to AdminUser
            var adminRoles = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string>
                {
                    RoleId = instructorRoleId,
                    UserId = adminUserId
                },
                new IdentityUserRole<string>
                {
                    RoleId = adminRoleId,
                    UserId = adminUserId
                },
                new IdentityUserRole<string>
                {
                    RoleId = instructorRoleId,
                    UserId = adminUserId
                }
            };

            builder.Entity<IdentityUserRole<string>>().HasData(adminRoles);
        }
    }
}
