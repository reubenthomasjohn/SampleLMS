using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SampleLMS.Dal.Interfaces;
using SampleLMS.Data;

namespace SampleLMS.Dal.Repositories
{
    public class UserRepository : IUserInterface
    {
        private readonly AuthDbContext authDbContext;

        public UserRepository(AuthDbContext authDbContext)
        {
            this.authDbContext = authDbContext;
        }

        public async Task<IEnumerable<IdentityUser>> GetAll()
        {
            var users = await authDbContext.Users.ToListAsync();
            var superAdminUser = await authDbContext.Users
                .FirstOrDefaultAsync(x => x.Email == "superadmin@bloggie.com");
            if (superAdminUser is not null)
            {
                users.Remove(superAdminUser);
            }
            return users;
        }
    }
}
