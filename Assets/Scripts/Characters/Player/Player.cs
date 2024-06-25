using UnityEngine;

[RequireComponent(typeof(InputHandler))]
[RequireComponent(typeof(ExperienceManager))]
public class Player : Character
{
    private InputHandler inputHandler;
    public ExperienceManager ExperienceManager { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        ExperienceManager = GetComponent<ExperienceManager>();
        SetDefaultMovement();
    }

    private void SetDefaultMovement()
    {
        if (inputHandler == null)
        {
            inputHandler = GetComponent<InputHandler>();
        }

        SetMovement(new InputMovement(inputHandler));
    }
}
