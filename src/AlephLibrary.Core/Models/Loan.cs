using System;
using System.ComponentModel.DataAnnotations;

namespace AlephLibrary.Core.Models
{
    public class Loan
    {
        public int Id { get; set; }
        [Required]
        public int BookId { get; set; }
        public Book? Book { get; set; }
        [Required]
        public int MemberId { get; set; }
        public Member? Member { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}
