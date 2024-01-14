using Practice.Models;

namespace Practice.Utilities;

public static class ExperienceManager
{
    public static int CalculateXpGainedFromQuest(Quest quest, Character character)
    {
        int baseXp = quest.Difficulty * 10;
        // TODO: implement logic for calculating XP based on character level
        // based on the character's level, the XP will be increased or decreased
        // character's performance, quest completion speed, etc. will also affect XP
        return baseXp;
    }

    public static bool IsEligibleForLevelUp(Character character)
    {
        int xpNeeded = XPThresholdForNextLevel(character.Level);
        return character.XP >= xpNeeded;
    }


    public static int XPNeededForNextLevel(Character character, bool isExponential = true)
    {
        int xpForNextLevel = XPThresholdForNextLevel(character.Level, isExponential);
        return xpForNextLevel - character.XP;
    }

    public static int XPThresholdForNextLevel(int currentLevel, bool isExponential = true)
    {
        if (isExponential)
        {
            return (int)Math.Round(100 * Math.Pow(1.1, currentLevel));
        }
        else
        {
            return 100 * currentLevel;
        }
    }

    public static void ApplyLevelUp(Character character)
    {
        character.Level++;
        character.SkillPoints++;
    }
}
