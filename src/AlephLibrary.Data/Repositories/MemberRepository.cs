using AlephLibrary.Core.Models;
using AlephLibrary.Core.Repositories;

namespace AlephLibrary.Data.Repositories
{
    public class MemberRepository : GenericRepository<Member>, IMemberRepository
    {
        public MemberRepository(DataContext ctx) : base(ctx) { }
    }
}
