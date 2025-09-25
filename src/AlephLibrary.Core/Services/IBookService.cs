using AlephLibrary.Core.Dtos;
namespace AlephLibrary.Core.Services
{
    public interface IBookService : ICrudService<BookDto, CreateBookDto, UpdateBookDto> { }
}
