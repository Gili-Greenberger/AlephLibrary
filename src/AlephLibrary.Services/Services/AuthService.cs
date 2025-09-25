using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using AlephLibrary.Core.Dtos;
using AlephLibrary.Core.Models;
using AlephLibrary.Core.Repositories;
using AlephLibrary.Core.Services;
using AlephLibrary.Data;

namespace AlephLibrary.Services.Services
{
    public class AuthService : IAuthService
    {
        private readonly DataContext _ctx;
        private readonly IConfiguration _config;
        private readonly IUserRepository _users;
        public AuthService(DataContext ctx, IConfiguration config, IUserRepository users) { _ctx = ctx; _config = config; _users = users; }

        private static string Sha256(string input)
        {
            using var sha = System.Security.Cryptography.SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(input));
            return BitConverter.ToString(bytes).Replace("-", string.Empty).ToLowerInvariant();
        }

        public async Task<LoginResponse> RegisterAsync(RegisterRequest req)
        {
            if (await _ctx.Users.AnyAsync(u => u.Username == req.Username))
                throw new InvalidOperationException("Username already exists");
            var user = new AppUser { Username = req.Username, PasswordHash = Sha256(req.Password), Role = req.Role };
            _ctx.Users.Add(user);
            await _ctx.SaveChangesAsync();
            return await LoginAsync(new LoginRequest { Username = req.Username, Password = req.Password });
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest req)
        {
            var user = await _ctx.Users.FirstOrDefaultAsync(u => u.Username == req.Username);
            if (user == null) throw new UnauthorizedAccessException("Invalid credentials");
            var hash = Sha256(req.Password);
            if (!string.Equals(user.PasswordHash, hash, StringComparison.OrdinalIgnoreCase))
                throw new UnauthorizedAccessException("Invalid credentials");

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]!);
            var claims = new[] {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role),
            };
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(double.Parse(_config["Jwt:ExpirationMinutes"] ?? "120")),
                Issuer = _config["Jwt:Issuer"],
                Audience = _config["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new LoginResponse { Token = tokenHandler.WriteToken(token), Username = user.Username, Role = user.Role };
        }
    }
}
