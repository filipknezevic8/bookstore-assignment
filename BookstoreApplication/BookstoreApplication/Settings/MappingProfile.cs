using AutoMapper;
using BookstoreApplication.DTOs;
using BookstoreApplication.Models;

namespace BookstoreApplication.Settings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Book, BookDto>()
                .ForMember(dest => dest.Age,
                    opt => opt.MapFrom(src => DateTime.Now.Year - src.PublishedDate.Year));

            CreateMap<Book, BookDetailsDto>();

            CreateMap<Author, AuthorDTO>().ReverseMap();

            CreateMap<RegistrationDto, ApplicationUser>();
        }
    }
}
