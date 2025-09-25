using System.ComponentModel.DataAnnotations;

namespace AlephLibrary.Core.Dtos
{
    public class AuthorDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Bio { get; set; }
        public int BooksCount { get; set; }
    }
    public class CreateAuthorDto
    {
        [Required, MaxLength(120)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(2000)]
        public string? Bio { get; set; }
    }
    public class UpdateAuthorDto
    {
        [MaxLength(120)]
        public string? Name { get; set; }
        [MaxLength(2000)]
        public string? Bio { get; set; }
    }
}
