﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Practice.Data;
using Practice.Dtos.Character;
using Practice.Enums;
using Practice.Models;
using Practice.Utilities;

namespace Practice.Services;

public class CharacterService : ICharacterService
{
    private readonly IMapper _mapper;
    private readonly DataContext _context;
    public CharacterService(IMapper mapper, DataContext context)
    {
        _mapper = mapper;
        _context = context;
    }
    
    public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
    {
        var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
        var dbCharacters = await _context.Characters.ToListAsync();
        serviceResponse.Data = dbCharacters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
    
        return serviceResponse;
    }

    public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
    {
        var serviceResponse = new ServiceResponse<GetCharacterDto>();
        var dbCharacter = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id);

        if (dbCharacter == null)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = "Character not found.";
            return serviceResponse;
        }
        
        // map the character to the GetCharacterDto with automapper
        serviceResponse.Data = _mapper.Map<GetCharacterDto>(dbCharacter);
        
        return serviceResponse;
    }

    public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
    {
        var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
        
        try
        {
            Character character = _mapper.Map<Character>(newCharacter);

            await _context.Characters.AddAsync(character);
            await _context.SaveChangesAsync();

            var dbCharacters = await _context.Characters.ToListAsync();
            serviceResponse.Data = dbCharacters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            serviceResponse.Message = "Character added successfully.";
        }
        catch (Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }
        return serviceResponse;
    }

    public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updateCharacter)
    {
        var serviceResponse = new ServiceResponse<GetCharacterDto>();
        try 
        {
            var character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == updateCharacter.Id);
        
            if (character == null)
            {
                throw new Exception("Character not found.");
            }

            _mapper.Map(updateCharacter, character);
            _context.Characters.Update(character);
            await _context.SaveChangesAsync();

            serviceResponse.Data = _mapper.Map<GetCharacterDto>(character);
            serviceResponse.Message = "Character updated successfully.";
        } 
        catch (Exception ex) 
        {
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
            var character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id);
        
            if (character == null)
            {
                throw new Exception("Character not found.");
            }

            _context.Characters.Remove(character);
            await _context.SaveChangesAsync();

            var dbCharacters = await _context.Characters.ToListAsync();
            serviceResponse.Data = dbCharacters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            serviceResponse.Message = "Character deleted successfully.";
        } 
        catch (Exception ex) 
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message; // Or a more user-friendly message
        }
    
        return serviceResponse;
    }

    public async Task<ServiceResponse<GetCharacterDto>> CompleteQuest(CompleteCharacterQuestDto completeQuest)
    {
        var serviceResponse = new ServiceResponse<GetCharacterDto>();

        try {
            var characterQuest = await _context.CharacterQuests
            .FirstOrDefaultAsync(cq => cq.CharacterId == completeQuest.CharacterId && cq.QuestId == completeQuest.QuestId);
            
            if (characterQuest == null ) {
                serviceResponse.Success = false;
                serviceResponse.Message = "Quest or character not found.";
                return serviceResponse;
            }

            if (characterQuest.IsCompleted) {
                serviceResponse.Success = false;
                serviceResponse.Message = "This quest has already been completed.";
                return serviceResponse;
            }
            
            characterQuest.IsCompleted = true;
            characterQuest.DateCompleted = DateTime.UtcNow;

            // calculate xp
            var quest = await _context.Quests.FirstOrDefaultAsync(q => q.Id == completeQuest.QuestId);
            
            if (quest == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Quest not found.";
                return serviceResponse;
            }
            
            // Award item rewards if any
            // TODO: Create inventory system
            // TODO: Add item rewards to character inventory
            // if (quest.ItemRewards.Any())
            // {
            //     foreach (var item in quest.ItemRewards)
            //     {
            //         characterQuest.Character.Item.Add(item);
            //     }
            // }

            int xpGained = ExperienceManager.CalculateXpGainedFromQuest(quest, characterQuest.Character);
            characterQuest.Character.XP += xpGained;

            // check if character is eligible for level up
            if (ExperienceManager.IsEligibleForLevelUp(characterQuest.Character)) {
                ExperienceManager.ApplyLevelUp(characterQuest.Character);
            }

            await _context.SaveChangesAsync();
            serviceResponse.Data = _mapper.Map<GetCharacterDto>(characterQuest.Character);

        } catch (Exception ex) {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }

        return serviceResponse;
    }

    public async Task<ServiceResponse<GetCharacterDto>> AllocateSkillPoint(AllocateSkillPointDto allocateSkillPointDto)
    {
    var serviceResponse = new ServiceResponse<GetCharacterDto>();
    
    try
    {
        var character = await _context.Characters.FindAsync(allocateSkillPointDto.CharacterId);

        if (character == null)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = "Character not found";
            return serviceResponse;
        }

        if (character.SkillPoints <= 0)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = "No skill points available to allocate";
            return serviceResponse;
        }

        if (!AllocateStatPoint(character, allocateSkillPointDto.StatName))
        {
            serviceResponse.Success = false;
            serviceResponse.Message = "Invalid stat name provided";
            return serviceResponse;
        }

        character.SkillPoints--;
        await _context.SaveChangesAsync();
        
        serviceResponse.Data = _mapper.Map<GetCharacterDto>(character);
        }
        catch (Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }

        return serviceResponse;
    }

    private static bool AllocateStatPoint(Character character, StatName statName)
    {
        switch (statName)
        {
            case StatName.Strength:
                character.Strength++;
                break;
            case StatName.Defense:
                character.Defense++;
                break;
            case StatName.Intelligence:
                character.Intelligence++;
                break;
            default:
                return false;
        }

        return true;
    }
}