using Practice.Dtos.Quest;
using Practice.Models;

namespace Practice.Services;

public interface IQuestService
{
    Task<ServiceResponse<List<GetQuestDto>>> GetAllQuests();
    Task<ServiceResponse<GetQuestDto>> GetQuestById(int id);
    Task<ServiceResponse<List<GetQuestDto>>> AddQuest(AddQuestDto newQuest);
    Task<ServiceResponse<GetQuestDto>> UpdateQuest(UpdateQuestDto updatedQuest);
    Task<ServiceResponse<List<GetQuestDto>>> DeleteQuest(int id);
}