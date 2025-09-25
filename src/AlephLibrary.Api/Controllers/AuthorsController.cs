using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AlephLibrary.Core.Dtos;
using AlephLibrary.Core.Services;

namespace AlephLibrary.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _svc;
        public AuthorsController(IAuthorService svc) => _svc = svc;

        [HttpGet]
        [AllowAnonymous]
        public Task<List<AuthorDto>> GetAll() => _svc.GetAllAsync();

        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<AuthorDto>> GetById(int id)
        {
            var e = await _svc.GetByIdAsync(id);
            return e == null ? NotFound() : Ok(e);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public Task<AuthorDto> Create(CreateAuthorDto dto) => _svc.AddAsync(dto);

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        public Task<AuthorDto> Update(int id, UpdateAuthorDto dto) => _svc.UpdateAsync(id, dto);

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id) { await _svc.DeleteAsync(id); return NoContent(); }
    }
}
