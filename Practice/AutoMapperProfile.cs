using AutoMapper;
using Practice.Dtos.Character;
using Practice.Dtos.Item;
using Practice.Dtos.Quest;
using Practice.Models;

namespace Practice;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Character, GetCharacterDto>();
        CreateMap<AddCharacterDto, Character>();
        CreateMap<Quest, GetQuestDto>();
        CreateMap<AddQuestDto, Quest>();
        CreateMap<UpdateQuestDto, Quest>();
        CreateMap<Item, GetItemDto>();
    }
}