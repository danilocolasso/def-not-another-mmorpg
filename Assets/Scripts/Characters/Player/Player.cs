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

    public Character enemyTest;
    protected void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SetTarget(enemyTest);
            BattleManager.Attack(enemyTest);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ExperienceManager.GainExperience(12);
        }
    }
}
