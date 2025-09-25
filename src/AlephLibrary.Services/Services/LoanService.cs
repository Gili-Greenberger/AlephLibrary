using System;
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
    public class LoanService : ILoanService
    {
        private readonly DataContext _ctx;
        private readonly ILoanRepository _repo;
        private readonly IMapper _mapper;
        public LoanService(DataContext ctx, ILoanRepository repo, IMapper mapper) { _ctx = ctx; _repo = repo; _mapper = mapper; }

        public async Task<List<LoanDto>> GetAllAsync() => await _ctx.Loans.Include(l => l.Book).Include(l => l.Member).AsNoTracking().ProjectTo<LoanDto>(_mapper.ConfigurationProvider).ToListAsync();

        public async Task<LoanDto?> GetByIdAsync(int id)
        {
            var e = await _ctx.Loans.Include(l => l.Book).Include(l => l.Member).AsNoTracking().FirstOrDefaultAsync(l => l.Id == id);
            return e == null ? null : _mapper.Map<LoanDto>(e);
        }

        public async Task<LoanDto> AddAsync(CreateLoanDto dto)
        {
            var book = await _ctx.Books.FirstOrDefaultAsync(b => b.Id == dto.BookId) ?? throw new KeyNotFoundException("Book not found");
            var member = await _ctx.Members.FirstOrDefaultAsync(m => m.Id == dto.MemberId) ?? throw new KeyNotFoundException("Member not found");
            if (book.CopiesAvailable <= 0) throw new InvalidOperationException("No copies available");

            book.CopiesAvailable -= 1;
            var now = DateTime.UtcNow;
            var loan = new Loan { BookId = book.Id, MemberId = member.Id, LoanDate = now, DueDate = now.AddDays(dto.LoanDays) };
            _ctx.Loans.Add(loan);
            await _ctx.SaveChangesAsync();
            return _mapper.Map<LoanDto>(loan);
        }

        public async Task<LoanDto> UpdateAsync(int id, UpdateLoanDto dto)
        {
            var e = await _ctx.Loans.Include(l => l.Book).FirstOrDefaultAsync(l => l.Id == id) ?? throw new KeyNotFoundException($"Loan {id} not found");
            if (dto.ReturnDate.HasValue && e.ReturnDate == null)
            {
                e.ReturnDate = dto.ReturnDate.Value;
                if (e.Book != null) e.Book.CopiesAvailable += 1;
            }
            if (dto.ExtendDays.HasValue && dto.ExtendDays.Value > 0)
            {
                e.DueDate = e.DueDate.AddDays(dto.ExtendDays.Value);
            }
            await _ctx.SaveChangesAsync();
            return _mapper.Map<LoanDto>(e);
        }

        public async Task DeleteAsync(int id)
        {
            var e = await _ctx.Loans.Include(l => l.Book).FirstOrDefaultAsync(l => l.Id == id);
            if (e != null)
            {
                if (e.ReturnDate == null && e.Book != null) e.Book.CopiesAvailable += 1;
                _ctx.Loans.Remove(e);
                await _ctx.SaveChangesAsync();
            }
        }
    }
}
