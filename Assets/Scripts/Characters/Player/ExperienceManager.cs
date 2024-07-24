using UnityEngine;

public class ExperienceManager : MonoBehaviour
{
    private const int FIRST_LEVEL_EXPERIENCE = 20;
    private const int EXPERIENCE_MULTIPLIER_PER_LEVEL = 10;

    private int currentExperience = 0;
    private int levelExperience = FIRST_LEVEL_EXPERIENCE;

    public int CurrentExperience => currentExperience;
    public int LevelExperience => levelExperience;

    public int Level { get; private set; } = 1;
    
    public void Initialize(Character character)
    {
        Level = character.Data.Level;
    }

    public void GainExperience(int amount)
    {
        int overExperience = currentExperience + amount - LevelExperience;
        currentExperience += amount;

        if (currentExperience >= LevelExperience)
        {
            LevelUp();
            currentExperience = overExperience;
        }
    }

    private void LevelUp()
    {
        Level++;
        levelExperience = CalculateQuadraticXP(Level, levelExperience, EXPERIENCE_MULTIPLIER_PER_LEVEL);
    }

    private int CalculateLinearXP(int level, int baseXP, int increment)
    {
        return baseXP + (increment * level);
    }

    private int CalculateExponentialXP(int level, int baseXP, float multiplier)
    {
        return (int)(baseXP * Mathf.Pow(multiplier, level - 1));
    }

    private int CalculateQuadraticXP(int level, int baseXP, int increment)
    {
        return baseXP + (increment * level * level);
    }
}
