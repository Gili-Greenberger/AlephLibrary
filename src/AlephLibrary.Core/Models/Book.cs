using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AlephLibrary.Core.Models
{
    public class Book
    {
        public int Id { get; set; }
        [Required, MaxLength(200)]
        public string Title { get; set; } = string.Empty;
        [Required, MaxLength(20)]
        public string Isbn { get; set; } = string.Empty;
        public int? AuthorId { get; set; }
        public Author? Author { get; set; }
        public int? GenreId { get; set; }
        public Genre? Genre { get; set; }
        [Range(0, 10000)]
        public int CopiesTotal { get; set; } = 1;
        [Range(0, 10000)]
        public int CopiesAvailable { get; set; } = 1;
        public ICollection<Loan> Loans { get; set; } = new List<Loan>();
    }
}
