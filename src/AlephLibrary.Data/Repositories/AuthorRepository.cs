using AlephLibrary.Core.Models;
using AlephLibrary.Core.Repositories;

namespace AlephLibrary.Data.Repositories
{
    public class AuthorRepository : GenericRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(DataContext ctx) : base(ctx) { }
    }
}
