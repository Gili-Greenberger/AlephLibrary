using AlephLibrary.Core.Dtos;
namespace AlephLibrary.Core.Services
{
    public interface ILoanService : ICrudService<LoanDto, CreateLoanDto, UpdateLoanDto> { }
}
