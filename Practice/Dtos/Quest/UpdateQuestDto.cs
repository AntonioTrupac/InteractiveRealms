namespace Practice.Dtos.Quest;

public class UpdateQuestDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Reward { get; set; }
    public int Difficulty { get; set; }
}