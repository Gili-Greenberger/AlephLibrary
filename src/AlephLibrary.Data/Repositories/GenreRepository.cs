using AlephLibrary.Core.Models;
using AlephLibrary.Core.Repositories;

namespace AlephLibrary.Data.Repositories
{
    public class GenreRepository : GenericRepository<Genre>, IGenreRepository
    {
        public GenreRepository(DataContext ctx) : base(ctx) { }
    }
}
