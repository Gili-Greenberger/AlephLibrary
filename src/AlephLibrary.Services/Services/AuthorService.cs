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
    public class AuthorService : IAuthorService
    {
        private readonly DataContext _ctx;
        private readonly IAuthorRepository _repo;
        private readonly IMapper _mapper;
        public AuthorService(DataContext ctx, IAuthorRepository repo, IMapper mapper) { _ctx = ctx; _repo = repo; _mapper = mapper; }

        public async Task<List<AuthorDto>> GetAllAsync() => await _ctx.Authors.Include(a => a.Books).AsNoTracking().ProjectTo<AuthorDto>(_mapper.ConfigurationProvider).ToListAsync();
        public async Task<AuthorDto?> GetByIdAsync(int id) { var e = await _ctx.Authors.Include(a => a.Books).AsNoTracking().FirstOrDefaultAsync(a => a.Id == id); return e == null ? null : _mapper.Map<AuthorDto>(e); }
        public async Task<AuthorDto> AddAsync(CreateAuthorDto dto) { var e = _mapper.Map<Author>(dto); var added = await _repo.AddAsync(e); return _mapper.Map<AuthorDto>(added); }
        public async Task<AuthorDto> UpdateAsync(int id, UpdateAuthorDto dto) { var e = await _repo.GetByIdAsync(id) ?? throw new KeyNotFoundException($"Author {id} not found"); _mapper.Map(dto, e); var updated = await _repo.UpdateAsync(e); return _mapper.Map<AuthorDto>(updated); }
        public async Task DeleteAsync(int id) => await _repo.DeleteAsync(id);
    }
}
