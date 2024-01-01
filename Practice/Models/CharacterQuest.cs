namespace Practice.Models;

public class CharacterQuest
{
    // Join table 
    public int CharacterId { get; set; }
    public Character Character { get; set; }
    
    public int QuestId { get; set; }
    public Quest Quest { get; set; }
    
    public DateTime DateAccepted { get; set; }
    public DateTime? DateCompleted { get; set; }
    public bool IsCompleted { get; set; }
    public int Progress { get; set; }
}