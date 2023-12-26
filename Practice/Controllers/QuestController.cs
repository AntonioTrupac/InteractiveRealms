using Microsoft.AspNetCore.Mvc;
using Practice.Dtos.Quest;
using Practice.Models;
using Practice.Services;

namespace Practice.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QuestController : ControllerBase
{
    private readonly IQuestService _questService;
    
    public QuestController(IQuestService questService)
    {
        _questService = questService;
    }
    
    [HttpGet("GetAll")]
    public async Task<ActionResult<ServiceResponse<List<GetQuestDto>>>> Get()
    {
        return Ok(await _questService.GetAllQuests());
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<ServiceResponse<GetQuestDto>>> GetSingle(int id)
    {
        return Ok(await _questService.GetQuestById(id));
    }
    
    [HttpPost]
    public async Task<ActionResult<ServiceResponse<List<GetQuestDto>>>> AddQuest(AddQuestDto newQuest)
    {
        return Ok(await _questService.AddQuest(newQuest));
    }

    [HttpPut]
    public async Task<ActionResult<ServiceResponse<GetQuestDto>>> UpdateQuest(UpdateQuestDto updatedQuest)
    {
        var response = await _questService.UpdateQuest(updatedQuest);

        if (!response.Success)
        {
            return NotFound(response);
        }
        
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ServiceResponse<List<GetQuestDto>>>> DeleteQuest(int id)
    {
        var response = await _questService.DeleteQuest(id);
        
        if (!response.Success)
        {
            return NotFound(response);
        }
        
        return Ok(response);
    }
}