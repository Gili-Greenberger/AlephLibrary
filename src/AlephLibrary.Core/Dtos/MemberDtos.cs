using System.ComponentModel.DataAnnotations;

namespace AlephLibrary.Core.Dtos
{
    public class MemberDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public int LoansCount { get; set; }
    }
    public class CreateMemberDto
    {
        [Required, MaxLength(150)]
        public string FullName { get; set; } = string.Empty;
        [Required, EmailAddress, MaxLength(256)]
        public string Email { get; set; } = string.Empty;
        [Phone, MaxLength(50)]
        public string? Phone { get; set; }
    }
    public class UpdateMemberDto
    {
        [MaxLength(150)]
        public string? FullName { get; set; }
        [EmailAddress, MaxLength(256)]
        public string? Email { get; set; }
        [Phone, MaxLength(50)]
        public string? Phone { get; set; }
    }
}
