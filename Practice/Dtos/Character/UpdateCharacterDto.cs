﻿using Practice.Models;

namespace Practice.Dtos.Character;

public class UpdateCharacterDto
{
    public int Id { get; set; }
    public string Name { get; set; } = "Frodo";
    public int HitPoints { get; set; } = 100;
    public int Strength { get; set; } = 10;
    public int Defense { get; set; } = 10;
    public int Intelligence { get; set; } = 10;
    public int XP { get; set; }
    public int Level { get; set; }
    public RpgClass Class { get; set; } = RpgClass.Fighter;
}