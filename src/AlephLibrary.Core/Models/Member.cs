using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AlephLibrary.Core.Models
{
    public class Member
    {
        public int Id { get; set; }
        [Required, MaxLength(150)]
        public string FullName { get; set; } = string.Empty;
        [Required, EmailAddress, MaxLength(256)]
        public string Email { get; set; } = string.Empty;
        [Phone, MaxLength(50)]
        public string? Phone { get; set; }
        public ICollection<Loan> Loans { get; set; } = new List<Loan>();
    }
}
