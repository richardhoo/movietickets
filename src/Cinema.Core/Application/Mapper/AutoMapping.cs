using AutoMapper;
using MovieTicketing.Application.Audience;
using MovieTicketing.Dto;

namespace MovieTicketing.Application.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CustomerDto, Customer>(); 
        }
    }
}
