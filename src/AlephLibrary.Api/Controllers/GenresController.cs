using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AlephLibrary.Core.Dtos;
using AlephLibrary.Core.Services;

namespace AlephLibrary.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenresController : ControllerBase
    {
        private readonly IGenreService _svc;
        public GenresController(IGenreService svc) => _svc = svc;

        [HttpGet]
        [AllowAnonymous]
        public Task<List<GenreDto>> GetAll() => _svc.GetAllAsync();

        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<GenreDto>> GetById(int id)
        {
            var e = await _svc.GetByIdAsync(id);
            return e == null ? NotFound() : Ok(e);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public Task<GenreDto> Create(CreateGenreDto dto) => _svc.AddAsync(dto);

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        public Task<GenreDto> Update(int id, UpdateGenreDto dto) => _svc.UpdateAsync(id, dto);

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id) { await _svc.DeleteAsync(id); return NoContent(); }
    }
}
