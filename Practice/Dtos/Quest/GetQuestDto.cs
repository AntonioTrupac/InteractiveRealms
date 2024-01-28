using Practice.Dtos.Item;

namespace Practice.Dtos.Quest;

public class GetQuestDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Reward { get; set; }
    public int Difficulty { get; set; }
    
    public List<GetItemDto> RewardPool { get; set; } = new List<GetItemDto>();

}