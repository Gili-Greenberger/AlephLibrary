using AlephLibrary.Core.Dtos;
namespace AlephLibrary.Core.Services
{
    public interface IMemberService : ICrudService<MemberDto, CreateMemberDto, UpdateMemberDto> { }
}
