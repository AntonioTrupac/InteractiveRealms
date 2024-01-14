using Practice.Dtos.Character;
using Practice.Models;

namespace Practice.Services;

public interface ICharacterService
{
    Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters();
    Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id);
    Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter);
    Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updateCharacter);
    Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id);
    Task<ServiceResponse<GetCharacterDto>> CompleteQuest(CompleteCharacterQuestDto completeQuest);
    Task<ServiceResponse<GetCharacterDto>> AllocateSkillPoint(AllocateSkillPointDto newCharacterSkill);
}