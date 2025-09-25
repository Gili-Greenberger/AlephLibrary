using System.ComponentModel.DataAnnotations;

namespace AlephLibrary.Core.Dtos
{
    public class GenreDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int BooksCount { get; set; }
    }
    public class CreateGenreDto
    {
        [Required, MaxLength(80)]
        public string Name { get; set; } = string.Empty;
    }
    public class UpdateGenreDto
    {
        [MaxLength(80)]
        public string? Name { get; set; }
    }
}
