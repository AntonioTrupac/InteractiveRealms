using AutoMapper;
using Practice.Dtos.Character;
using Practice.Models;

namespace Practice;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Character, GetCharacterDto>();
        CreateMap<AddCharacterDto, Character>();
    }
}