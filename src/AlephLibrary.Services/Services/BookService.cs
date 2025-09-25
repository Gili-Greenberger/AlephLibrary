using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using AlephLibrary.Core.Dtos;
using AlephLibrary.Core.Models;
using AlephLibrary.Core.Repositories;
using AlephLibrary.Core.Services;
using AlephLibrary.Data;

namespace AlephLibrary.Services.Services
{
    public class BookService : IBookService
    {
        private readonly DataContext _ctx;
        private readonly IBookRepository _repo;
        private readonly IMapper _mapper;
        public BookService(DataContext ctx, IBookRepository repo, IMapper mapper) { _ctx = ctx; _repo = repo; _mapper = mapper; }

        public async Task<List<BookDto>> GetAllAsync() => await _ctx.Books.Include(b => b.Author).Include(b => b.Genre).AsNoTracking().ProjectTo<BookDto>(_mapper.ConfigurationProvider).ToListAsync();
        public async Task<BookDto?> GetByIdAsync(int id)
        {
            var b = await _ctx.Books.Include(x => x.Author).Include(x => x.Genre).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return b == null ? null : _mapper.Map<BookDto>(b);
        }
        public async Task<BookDto> AddAsync(CreateBookDto dto) { var e = _mapper.Map<Book>(dto); var added = await _repo.AddAsync(e); return _mapper.Map<BookDto>(added); }
        public async Task<BookDto> UpdateAsync(int id, UpdateBookDto dto)
        {
            var e = await _repo.GetByIdAsync(id) ?? throw new KeyNotFoundException($"Book {id} not found");
            _mapper.Map(dto, e);
            if (dto.CopiesTotal.HasValue && !dto.CopiesAvailable.HasValue && e.CopiesAvailable > e.CopiesTotal) e.CopiesAvailable = e.CopiesTotal;
            var updated = await _repo.UpdateAsync(e);
            return _mapper.Map<BookDto>(updated);
        }
        public async Task DeleteAsync(int id) => await _repo.DeleteAsync(id);
    }
}
