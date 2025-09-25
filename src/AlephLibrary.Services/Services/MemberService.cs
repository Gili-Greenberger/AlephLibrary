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
    public class MemberService : IMemberService
    {
        private readonly DataContext _ctx;
        private readonly IMemberRepository _repo;
        private readonly IMapper _mapper;
        public MemberService(DataContext ctx, IMemberRepository repo, IMapper mapper) { _ctx = ctx; _repo = repo; _mapper = mapper; }

        public async Task<List<MemberDto>> GetAllAsync() => await _ctx.Members.Include(m => m.Loans).AsNoTracking().ProjectTo<MemberDto>(_mapper.ConfigurationProvider).ToListAsync();
        public async Task<MemberDto?> GetByIdAsync(int id) { var e = await _ctx.Members.Include(m => m.Loans).AsNoTracking().FirstOrDefaultAsync(m => m.Id == id); return e == null ? null : _mapper.Map<MemberDto>(e); }
        public async Task<MemberDto> AddAsync(CreateMemberDto dto) { var e = _mapper.Map<Member>(dto); var added = await _repo.AddAsync(e); return _mapper.Map<MemberDto>(added); }
        public async Task<MemberDto> UpdateAsync(int id, UpdateMemberDto dto) { var e = await _repo.GetByIdAsync(id) ?? throw new KeyNotFoundException($"Member {id} not found"); _mapper.Map(dto, e); var updated = await _repo.UpdateAsync(e); return _mapper.Map<MemberDto>(updated); }
        public async Task DeleteAsync(int id) => await _repo.DeleteAsync(id);
    }
}
