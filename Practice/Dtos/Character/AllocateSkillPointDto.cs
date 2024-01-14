using Practice.Enums;

namespace Practice.Dtos.Character
{
    public class AllocateSkillPointDto
    {
        public int CharacterId { get; set; }
        public StatName StatName { get; set; }
    }
}