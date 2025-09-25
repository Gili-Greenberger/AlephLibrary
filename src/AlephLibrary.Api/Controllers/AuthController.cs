using Microsoft.AspNetCore.Mvc;
using AlephLibrary.Core.Dtos;
using AlephLibrary.Core.Services;

namespace AlephLibrary.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _auth;
        public AuthController(IAuthService auth) => _auth = auth;

        [HttpPost("register")]
        public Task<LoginResponse> Register(RegisterRequest req) => _auth.RegisterAsync(req);

        [HttpPost("login")]
        public Task<LoginResponse> Login(LoginRequest req) => _auth.LoginAsync(req);
    }
}
