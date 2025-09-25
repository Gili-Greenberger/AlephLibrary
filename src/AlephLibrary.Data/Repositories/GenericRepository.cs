using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AlephLibrary.Core.Repositories;

namespace AlephLibrary.Data.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly DataContext _ctx;
        protected readonly DbSet<T> _set;
        public GenericRepository(DataContext ctx) { _ctx = ctx; _set = _ctx.Set<T>(); }

        public async Task<List<T>> GetAllAsync() => await _set.AsNoTracking().ToListAsync();
        public async Task<T?> GetByIdAsync(int id) => await _set.FindAsync(id);
        public async Task<T> AddAsync(T entity) { _set.Add(entity); await _ctx.SaveChangesAsync(); return entity; }
        public async Task<T> UpdateAsync(T entity) { _ctx.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified; await _ctx.SaveChangesAsync(); return entity; }
        public async Task DeleteAsync(int id)
        {
            var e = await _set.FindAsync(id);
            if (e != null) { _set.Remove(e); await _ctx.SaveChangesAsync(); }
        }
    }
}
