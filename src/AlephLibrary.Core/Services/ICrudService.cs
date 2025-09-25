using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlephLibrary.Core.Services
{
    public interface ICrudService<TDto, TCreateDto, TUpdateDto>
    {
        Task<List<TDto>> GetAllAsync();
        Task<TDto?> GetByIdAsync(int id);
        Task<TDto> AddAsync(TCreateDto dto);
        Task<TDto> UpdateAsync(int id, TUpdateDto dto);
        Task DeleteAsync(int id);
    }
}
