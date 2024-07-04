using UnityEngine;

[RequireComponent(typeof(ExperienceManager))]
[RequireComponent(typeof(InputHandler))]
public class Player : Character
{
    private InputHandler inputHandler;
    public ExperienceManager ExperienceManager { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        ExperienceManager = GetComponent<ExperienceManager>();
        inputHandler = GetComponent<InputHandler>();

        SetMovement(new InputMovement(inputHandler));
    }

    protected void Update()
    {
        if (!Target) return;
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            battleManager.Attack(Target);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ExperienceManager.GainExperience(12);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            abilityManager.UseAbility("Arrow Shot", this, Target);
        }
    }
}
