using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AlephLibrary.Core.Models;
using AlephLibrary.Core.Repositories;

namespace AlephLibrary.Data.Repositories
{
    public class UserRepository : GenericRepository<AppUser>, IUserRepository
    {
        public UserRepository(DataContext ctx) : base(ctx) { }
        public async Task<AppUser?> GetByUsernameAsync(string username) => await _ctx.Users.FirstOrDefaultAsync(u => u.Username == username);
    }
}
