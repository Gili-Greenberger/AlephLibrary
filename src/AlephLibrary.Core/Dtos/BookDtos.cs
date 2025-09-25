using System.ComponentModel.DataAnnotations;

namespace AlephLibrary.Core.Dtos
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Isbn { get; set; } = string.Empty;
        public string? AuthorName { get; set; }
        public string? GenreName { get; set; }
        public int CopiesAvailable { get; set; }
    }
    public class CreateBookDto
    {
        [Required, MaxLength(200)]
        public string Title { get; set; } = string.Empty;
        [Required, MaxLength(20)]
        public string Isbn { get; set; } = string.Empty;
        public int? AuthorId { get; set; }
        public int? GenreId { get; set; }
        [Range(1, 10000)]
        public int CopiesTotal { get; set; } = 1;
    }
    public class UpdateBookDto
    {
        [MaxLength(200)]
        public string? Title { get; set; }
        [MaxLength(20)]
        public string? Isbn { get; set; }
        public int? AuthorId { get; set; }
        public int? GenreId { get; set; }
        public int? CopiesTotal { get; set; }
        public int? CopiesAvailable { get; set; }
    }
}
