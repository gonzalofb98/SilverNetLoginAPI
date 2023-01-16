using AutoMapper;
using Dto.Request;
using Dto.Response;
using Entities;

namespace Services.Mapper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Book, DtoBookResponse>()
                .ForMember(dest => dest.ReleaseDate, opt => opt.MapFrom(src => src.ReleaseDate));
            CreateMap<DtoBookResponse, Book>();
            CreateMap<DtoBookRequest, Book>();
            CreateMap<Book, DtoBookRequest>();
        }
    }
}
