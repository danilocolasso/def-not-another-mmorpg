using UnityEngine;

[RequireComponent(typeof(InputHandler))]
public class Player : Character
{
    private const float EXPERIENCE_MULTIPLIER_PER_LEVEL = 1.2f;

    private InputHandler inputHandler;

    public Status experience;

    protected override void Awake()
    {
        base.Awake();

        SetDefaultMovement();
        SetExperience();
    }

    public void GainExperience(float amount)
    {
        experience.Increment(amount);

        if (experience.Current == experience.Max)
        {
            StatusManager.LevelUp();
            experience.SetCurrent(0);
            experience.SetMax(experience.Max * EXPERIENCE_MULTIPLIER_PER_LEVEL);
        }
    }

    private void SetDefaultMovement()
    {
        if (inputHandler == null)
        {
            inputHandler = GetComponent<InputHandler>();
        }

        SetMovement(new InputMovement(inputHandler));
    }

    private void SetExperience()
    {
        experience = new Status(0, 100, 100);
    }
}
