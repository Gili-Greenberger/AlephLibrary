using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AlephLibrary.Core.Models
{
    public class Author
    {
        public int Id { get; set; }
        [Required, MaxLength(120)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(2000)]
        public string? Bio { get; set; }
        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
