using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Practice.Dtos.Character;
using Practice.Models;

namespace Practice.Services;

public class CharacterService : ICharacterService
{
    private static List<Character> _characters = new List<Character>()
    {
        new Character(),
        new Character { Name = "Sam", Id = 1, Class = RpgClass.Rogue, Defense = 20, Intelligence = 35, HitPoints = 15, Strength = 25 },
    };

    private readonly IMapper _mapper;
    public CharacterService(IMapper mapper)
    {
        _mapper = mapper;
    }
    
    public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
    {
        var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
        serviceResponse.Data = _characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
    
        return serviceResponse;
    }

    public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
    {
        var serviceResponse = new ServiceResponse<GetCharacterDto>();
        
        var character = _characters.FirstOrDefault(c => c.Id == id);

        if (character == null)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = "Character not found.";
            return serviceResponse;
        }
        
        // map the character to the GetCharacterDto with automapper
        serviceResponse.Data = _mapper.Map<GetCharacterDto>(character);
        
        return serviceResponse;
    }

    public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
    {
        var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
        var character = _mapper.Map<Character>(newCharacter);
        character.Id = _characters.Max(c => c.Id) + 1;
        _characters.Add(character);

        serviceResponse.Data = _characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
        return serviceResponse;
    }

    public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updateCharacter)
    {
        var serviceResponse = new ServiceResponse<GetCharacterDto>();
        try 
        {
            var character = _characters.FirstOrDefault(c => c.Id == updateCharacter.Id);
        
            if(character is null)
                throw new Exception($"Character with ${updateCharacter.Id} not found.");
            
            // this can be done with automapper as well, and probably is cleaner
            character.Name = updateCharacter.Name;
            character.Defense = updateCharacter.Defense;
            character.HitPoints = updateCharacter.HitPoints;
            character.Intelligence = updateCharacter.Intelligence;
            character.Strength = updateCharacter.Strength;
            character.Class = updateCharacter.Class;


            serviceResponse.Data = _mapper.Map<GetCharacterDto>(character);
        } catch (Exception ex) {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }
        
        return serviceResponse;
    }

    public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
    {
        var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
        try
        {
            var character = _characters.FirstOrDefault(c => c.Id == id);
        
            if(character is null)
                throw new Exception($"Character with ${id} not found.");
            
            _characters.Remove(character);
            serviceResponse.Data = _characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
        } catch (Exception ex) {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }
        
        return serviceResponse;
    }
}