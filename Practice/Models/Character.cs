using System.ComponentModel.DataAnnotations;

namespace Practice.Models;

public class Character
{
    public int Id { get; init; }
    [StringLength(100)]
    public string Name { get; init; } = "Frodo";
    public int HitPoints { get; set; } = 100;
    public int Strength { get; set; } = 10;
    public int Defense { get; set; } = 10;
    public int Intelligence { get; set; } = 10;
    public RpgClass Class { get; init; } = RpgClass.Fighter;

    public int XP { get; set; } = 0;
    public int Level { get; set; } = 1;
    public int SkillPoints { get; set; } = 0;
    public List<CharacterQuest> CharacterQuests { get; set; } = new List<CharacterQuest>();
    public ICollection<Item> Item { get; set; } = new List<Item>();
}