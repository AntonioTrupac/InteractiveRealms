using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Practice.Data;
using Practice.Dtos.Item;
using Practice.Dtos.Quest;
using Practice.Models;

namespace Practice.Services;

public class QuestService : IQuestService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public QuestService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ServiceResponse<List<GetQuestDto>>> GetAllQuests()
    {
        var serviceResponse = new ServiceResponse<List<GetQuestDto>>();
        try
        {
            var dbQuests = await _context.Quests.Include(q => q.RewardPool)
                .ToListAsync();
            serviceResponse.Data = _mapper.Map<List<GetQuestDto>>(dbQuests);
        } 
        catch (Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }
        return serviceResponse;
    }

    public async Task<ServiceResponse<GetQuestDto>> GetQuestById(int id)
    {
        var serviceResponse = new ServiceResponse<GetQuestDto>();
        try
        {
            var dbQuest = await _context.Quests.Include(q => q.RewardPool)
                .FirstOrDefaultAsync(q => q.Id == id);
            
            if (dbQuest != null)
            {   
                var questDto = _mapper.Map<GetQuestDto>(dbQuest);
                questDto.RewardPool = dbQuest.RewardPool.Select(item => _mapper.Map<GetItemDto>(item)).ToList();
                serviceResponse.Data = questDto;
            }
            else
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Quest not found.";
            }
        } 
        catch (Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }
        return serviceResponse;
    }

    public async Task<ServiceResponse<List<GetQuestDto>>> AddQuest(AddQuestDto newQuest)
    {
        var serviceResponse = new ServiceResponse<List<GetQuestDto>>();
        try
        {
           Quest quest = _mapper.Map<Quest>(newQuest);
           await _context.Quests.AddAsync(quest);
           await _context.SaveChangesAsync();
            
           serviceResponse.Data = await _context.Quests
               .Select(q => _mapper.Map<GetQuestDto>(q))
               .ToListAsync();
        } 
        catch (Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }
        return serviceResponse;
    }

    public async Task<ServiceResponse<GetQuestDto>> UpdateQuest(UpdateQuestDto updatedQuest)
    {
        var serviceResponse = new ServiceResponse<GetQuestDto>();
        try
        {
            var quest = await _context.Quests.FindAsync(updatedQuest.Id);
            if (quest != null)
            {
                _mapper.Map(updatedQuest, quest);
                _context.Quests.Update(quest);
                await _context.SaveChangesAsync();

                serviceResponse.Data = _mapper.Map<GetQuestDto>(quest);
            }
            else
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Quest not found.";
            }
        }
        catch (Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }
        return serviceResponse;
    }

    public async Task<ServiceResponse<List<GetQuestDto>>> DeleteQuest(int id)
    {
        var serviceResponse = new ServiceResponse<List<GetQuestDto>>();
        try
        {
            var quest = await _context.Quests.FindAsync(id);
            if (quest != null)
            {
                _context.Quests.Remove(quest);
                await _context.SaveChangesAsync();

                serviceResponse.Data = await _context.Quests
                    .Select(q => _mapper.Map<GetQuestDto>(q))
                    .ToListAsync();
            }
            else
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Quest not found.";
            }
        }
        catch (Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }
        return serviceResponse;
    }

    public async Task<ServiceResponse<GetQuestDto>> SaveOrUpdateQuest(AddQuestDto newQuest)
    {
        var serviceResponse = new ServiceResponse<GetQuestDto>();
        try
        {
            Quest quest = await _context.Quests.Include(q => q.RewardPool)
                              .FirstOrDefaultAsync(q => q.Id == newQuest.Id) ?? 
                          new Quest();
            
            _mapper.Map(newQuest, quest);
        
            if (newQuest.RewardPoolIds != null && newQuest.RewardPoolIds.Count > 0)
            {
                var rewardItems = await _context.Items
                    .Where(item => newQuest.RewardPoolIds.Contains(item.Id))
                    .ToListAsync();
                quest.RewardPool = rewardItems;
            }
        
            if (quest.Id == 0)
            {
                await _context.Quests.AddAsync(quest);
            }
            else
            {
                _context.Quests.Update(quest);
            }

            await _context.SaveChangesAsync();
            serviceResponse.Data = _mapper.Map<GetQuestDto>(quest);
        } 
        catch (Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }

        return serviceResponse;
    }
}
