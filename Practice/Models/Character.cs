namespace Practice.Models;

public class Character
{
    public int Id { get; set; }
    public string Name { get; set; } = "Frodo";
    public int HitPoints { get; set; } = 100;
    public int Strength { get; set; } = 10;
    public int Defense { get; set; } = 10;
    public int Intelligence { get; set; } = 10;
    public RpgClass Class { get; set; } = RpgClass.Fighter;

    public int XP { get; set; } = 0;
    public int Level { get; set; } = 1;
    
    public List<CharacterQuest>  CharacterQuests { get; set; }
}