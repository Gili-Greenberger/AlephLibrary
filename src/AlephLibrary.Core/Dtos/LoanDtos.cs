using System;
using System.ComponentModel.DataAnnotations;

namespace AlephLibrary.Core.Dtos
{
    public class LoanDto
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int MemberId { get; set; }
        public string BookTitle { get; set; } = string.Empty;
        public string MemberName { get; set; } = string.Empty;
        public DateTime LoanDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
    public class CreateLoanDto
    {
        [Required] public int BookId { get; set; }
        [Required] public int MemberId { get; set; }
        public int LoanDays { get; set; } = 14;
    }
    public class UpdateLoanDto
    {
        public DateTime? ReturnDate { get; set; }
        public int? ExtendDays { get; set; }
    }
}
