using AlephLibrary.Core.Models;
using System.Threading.Tasks;

namespace AlephLibrary.Core.Repositories
{
    public interface IUserRepository : IGenericRepository<AppUser>
    {
        Task<AppUser?> GetByUsernameAsync(string username);
    }
}
