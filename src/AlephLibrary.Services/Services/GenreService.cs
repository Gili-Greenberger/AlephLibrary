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
    public class GenreService : IGenreService
    {
        private readonly DataContext _ctx;
        private readonly IGenreRepository _repo;
        private readonly IMapper _mapper;
        public GenreService(DataContext ctx, IGenreRepository repo, IMapper mapper) { _ctx = ctx; _repo = repo; _mapper = mapper; }

        public async Task<List<GenreDto>> GetAllAsync() => await _ctx.Genres.Include(g => g.Books).AsNoTracking().ProjectTo<GenreDto>(_mapper.ConfigurationProvider).ToListAsync();
        public async Task<GenreDto?> GetByIdAsync(int id) { var e = await _ctx.Genres.Include(g => g.Books).AsNoTracking().FirstOrDefaultAsync(g => g.Id == id); return e == null ? null : _mapper.Map<GenreDto>(e); }
        public async Task<GenreDto> AddAsync(CreateGenreDto dto) { var e = _mapper.Map<Genre>(dto); var added = await _repo.AddAsync(e); return _mapper.Map<GenreDto>(added); }
        public async Task<GenreDto> UpdateAsync(int id, UpdateGenreDto dto) { var e = await _repo.GetByIdAsync(id) ?? throw new KeyNotFoundException($"Genre {id} not found"); _mapper.Map(dto, e); var updated = await _repo.UpdateAsync(e); return _mapper.Map<GenreDto>(updated); }
        public async Task DeleteAsync(int id) => await _repo.DeleteAsync(id);
    }
}
