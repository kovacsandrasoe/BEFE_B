using AutoMapper;
using BEFE_B.Dtos.Author;
using BEFE_B.Models;

namespace BEFE_B.Tools
{
    public class DtoProfile : Profile
    {
        public DtoProfile() 
        {
            CreateMap<AuthorCreateUpdateDto, Author>();
            CreateMap<Author, AuthorViewDto>();
        }
    }
}
