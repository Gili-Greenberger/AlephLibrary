using AlephLibrary.Core.Models;
using AlephLibrary.Core.Repositories;

namespace AlephLibrary.Data.Repositories
{
    public class LoanRepository : GenericRepository<Loan>, ILoanRepository
    {
        public LoanRepository(DataContext ctx) : base(ctx) { }
    }
}
