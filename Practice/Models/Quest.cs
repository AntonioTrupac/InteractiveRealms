﻿namespace Practice.Models;

public class Quest
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int Reward { get; set; }
    public int Difficulty { get; set; }
    public List<CharacterQuest>? CharacterQuests { get; set; }
    public List<Item> RewardPool { get; set; } = new List<Item>();
}