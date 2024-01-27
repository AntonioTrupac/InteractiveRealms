namespace Practice.Dtos.Quest;

public class AddQuestDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Reward { get; set; }
    public int Difficulty { get; set; }
    public List<int>? RewardPoolIds { get; set; } = new List<int>();
}