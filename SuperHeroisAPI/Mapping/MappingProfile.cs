using AutoMapper;
using SuperHeroisAPI.Dtos;
using SuperHeroisAPI.Models;

namespace SuperHeroisAPI.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Heroi, HeroiDto>().ReverseMap();
        }
    }
}
