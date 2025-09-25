using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AlephLibrary.Core.Models
{
    public class Genre
    {
        public int Id { get; set; }
        [Required, MaxLength(80)]
        public string Name { get; set; } = string.Empty;
        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
