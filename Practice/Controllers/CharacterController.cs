﻿using Microsoft.AspNetCore.Mvc;
using Practice.Dtos.Character;
using Practice.Models;
using Practice.Services;

namespace Practice.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CharacterController : ControllerBase
{
    private readonly ICharacterService _characterService;
    public CharacterController(ICharacterService characterService)
    {
        _characterService = characterService;
    }
    
    [HttpGet("GetAll")]
    public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> Get()
    {
        return Ok(await _characterService.GetAllCharacters());
    }
    
    [HttpGet(Name = "GetSingle")]
    public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> GetSingle(int id)
    {
        return Ok(await _characterService.GetCharacterById(id));
    }
    
    [HttpPost("AddCharacter")]
    public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> AddCharacter(AddCharacterDto newCharacter)
    {
        return Ok(await _characterService.AddCharacter(newCharacter));
    }

    [HttpPost("CompleteQuest")]
    public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> CompleteQuest(CompleteCharacterQuestDto completeQuestDto)
    {
        return Ok(await _characterService.CompleteQuest(completeQuestDto));
    }
    
    [HttpPut]
    public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> UpdateCharacter(UpdateCharacterDto updateCharacter)
    {
        var response = await _characterService.UpdateCharacter(updateCharacter);
    
        if (!response.Success)
        {
            return NotFound(response);
        }
    
        return Ok(response);
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> DeleteCharacter(int id)
    {
        var response = await _characterService.DeleteCharacter(id);
    
        if (!response.Success)
        {
            return NotFound(response);
        }
    
        return Ok(response);
    }

    [HttpPost("AllocateSkillPoint")]
    public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> AllocateSkillPoint(AllocateSkillPointDto allocateSkillPointDto )
    {
        var response = await _characterService.AllocateSkillPoint(allocateSkillPointDto);

        if (!response.Success)
        {
            return NotFound(response);
        }

        return Ok(response);
    }
}