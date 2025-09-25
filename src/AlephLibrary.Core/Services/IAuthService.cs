using System.Threading.Tasks;
using AlephLibrary.Core.Dtos;

namespace AlephLibrary.Core.Services
{
    public interface IAuthService
    {
        Task<LoginResponse> RegisterAsync(RegisterRequest req);
        Task<LoginResponse> LoginAsync(LoginRequest req);
    }
}
