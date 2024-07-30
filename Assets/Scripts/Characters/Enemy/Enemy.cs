using UnityEngine;

[RequireComponent(typeof(AggroManager))]
public class Enemy : Character
{
    protected AggroManager aggroManager;

    protected override void Awake()
    {
        base.Awake();
        aggroManager = GetComponent<AggroManager>();
    }

    public override void SetState(IState state)
    {
        base.SetState(state);

        if (state is InBattleState)
        {
            StartChase();
        }
        else
        {
            StopChase();
        }
    }

    private void StartChase()
    {
        movementManager.SetMovement(new TargetMovement(Target.transform, Status.AttackRange));
    }

    private void StopChase()
    {
        movementManager.SetMovement(null);
    }

    public override void EnterBattle(Character target)
    {
        base.EnterBattle(target);

        aggroManager.Disable();
    }

    public override void ExitBattle(Character target)
    {
        base.ExitBattle(target);

        if (IsAlive)
        {
            aggroManager.Enable();
        }
    }
}
