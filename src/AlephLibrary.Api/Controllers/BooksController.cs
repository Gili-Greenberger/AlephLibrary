using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AlephLibrary.Core.Dtos;
using AlephLibrary.Core.Services;

namespace AlephLibrary.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _svc;
        public BooksController(IBookService svc) => _svc = svc;

        [HttpGet]
        [AllowAnonymous]
        public Task<List<BookDto>> GetAll() => _svc.GetAllAsync();

        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<BookDto>> GetById(int id)
        {
            var b = await _svc.GetByIdAsync(id);
            return b == null ? NotFound() : Ok(b);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public Task<BookDto> Create(CreateBookDto dto) => _svc.AddAsync(dto);

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        public Task<BookDto> Update(int id, UpdateBookDto dto) => _svc.UpdateAsync(id, dto);

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id) { await _svc.DeleteAsync(id); return NoContent(); }
    }
}
