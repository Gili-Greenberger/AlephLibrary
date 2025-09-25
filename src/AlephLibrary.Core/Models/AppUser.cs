using System.ComponentModel.DataAnnotations;

namespace AlephLibrary.Core.Models
{
    public class AppUser
    {
        public int Id { get; set; }
        [Required, MaxLength(60)]
        public string Username { get; set; } = string.Empty;
        [Required]
        public string PasswordHash { get; set; } = string.Empty;
        [Required]
        public string Role { get; set; } = "User";
    }
}
