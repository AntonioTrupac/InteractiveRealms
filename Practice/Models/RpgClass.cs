using System.Text.Json.Serialization;

namespace Practice.Models;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum RpgClass
{
    Mage = 1,
    Healer = 2,
    Fighter = 3,
    Ranger = 4,
    Cleric = 5,
    Rogue = 6
}