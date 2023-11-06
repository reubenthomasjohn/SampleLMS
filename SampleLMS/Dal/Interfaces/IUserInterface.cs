using Microsoft.AspNetCore.Identity;

namespace SampleLMS.Dal.Interfaces
{
    public interface IUserInterface
    {
        Task<IEnumerable<IdentityUser>> GetAll();
    }
}
