using AlephLibrary.Core.Dtos;
namespace AlephLibrary.Core.Services
{
    public interface IAuthorService : ICrudService<AuthorDto, CreateAuthorDto, UpdateAuthorDto> { }
}
