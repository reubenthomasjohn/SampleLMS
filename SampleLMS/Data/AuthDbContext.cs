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

            // Seed Roles (Admin, Instructor, Student)

            var adminRoleId = "15428a96-ca04-4bec-9a65-433be902df74";
            var instructorRoleId = "99dcdbe9-f224-488f-aaf4-564dd053d0cd";
            var studentRoleId = "5ed18f35-4e37-428a-b5fa-fe3fd09411d1";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "Admin",
                    Id = adminRoleId,
                    ConcurrencyStamp = adminRoleId
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

            // Seed AdminUser
            var adminUserId = "4b00b78c-ddc0-43e4-b026-a6ac5e76c6e3";
            var adminUser = new IdentityUser
            {
                //FirstName = "Reuben",
                //LastName = "Thomas",
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
                    RoleId = studentRoleId,
                    UserId = adminUserId
                }
            };

            builder.Entity<IdentityUserRole<string>>().HasData(adminRoles);
        }
    }
}
