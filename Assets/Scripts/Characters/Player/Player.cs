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

    public Character enemyTest;
    protected void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SetTarget(enemyTest);
            battleManager.Attack(enemyTest);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ExperienceManager.GainExperience(12);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            abilityManager.UseAbility("Arrow Shot", this, enemyTest);
        }
    }
}
