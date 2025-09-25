using AutoMapper;
using AlephLibrary.Core.Models;
using AlephLibrary.Core.Dtos;

namespace AlephLibrary.Core.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Book, BookDto>()
                .ForMember(d => d.AuthorName, o => o.MapFrom(s => s.Author != null ? s.Author.Name : null))
                .ForMember(d => d.GenreName,  o => o.MapFrom(s => s.Genre != null ? s.Genre.Name : null));
            CreateMap<CreateBookDto, Book>()
                .ForMember(d => d.CopiesAvailable, o => o.MapFrom(s => s.CopiesTotal));
            CreateMap<UpdateBookDto, Book>();

            CreateMap<Author, AuthorDto>()
                .ForMember(d => d.BooksCount, o => o.MapFrom(s => s.Books.Count));
            CreateMap<CreateAuthorDto, Author>();
            CreateMap<UpdateAuthorDto, Author>();

            CreateMap<Genre, GenreDto>()
                .ForMember(d => d.BooksCount, o => o.MapFrom(s => s.Books.Count));
            CreateMap<CreateGenreDto, Genre>();
            CreateMap<UpdateGenreDto, Genre>();

            CreateMap<Member, MemberDto>()
                .ForMember(d => d.LoansCount, o => o.MapFrom(s => s.Loans.Count));
            CreateMap<CreateMemberDto, Member>();
            CreateMap<UpdateMemberDto, Member>();

            CreateMap<Loan, LoanDto>()
                .ForMember(d => d.BookTitle, o => o.MapFrom(s => s.Book != null ? s.Book.Title : string.Empty))
                .ForMember(d => d.MemberName, o => o.MapFrom(s => s.Member != null ? s.Member.FullName : string.Empty));
        }
    }
}
