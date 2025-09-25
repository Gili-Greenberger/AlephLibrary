using AlephLibrary.Core.Models;
using AlephLibrary.Core.Repositories;

namespace AlephLibrary.Data.Repositories
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        public BookRepository(DataContext ctx) : base(ctx) { }
    }
}
