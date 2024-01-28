using Practice.Enums;

namespace Practice.Dtos.Item;

public class GetItemDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public ItemType Type { get; set; }
    public ItemRarity Rarity { get; set; }
    public int Price { get; set; }
    public int? BonusStrength { get; set; }
    public int? BonusIntelligence { get; set; }

    public int? QuestId { get; set; }
    public string? QuestName { get; set; }
}
