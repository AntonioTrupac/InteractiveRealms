using System.ComponentModel.DataAnnotations;
using Practice.Enums;

namespace Practice.Models;

public class Item
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public ItemType Type { get; set; }
    public ItemRarity Rarity { get; set; }
    public int Price { get; set; }
    public int? BonusStrength { get; set; }
    public int? BonusIntelligence { get; set; }

    public int? CharacterId { get; set; }
    public Character? Character { get; set; }

    public int? QuestId { get; set; }
    public Quest? Quest { get; set; }
}