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
            StartChaseTarget();
        }
        else
        {
            StopChaseTarget();
        }
    }

    private void StartChaseTarget()
    {
        SetMovement(new TargetMovement(Target.transform, Status.AttackRange));
    }

    private void StopChaseTarget()
    {
        SetMovement(null);
    }
    
    public override void EnterBattle(Character target)
    {
        base.EnterBattle(target);
        aggroManager.Disable();
    }

    public override void ExitBattle(Character target)
    {
        base.ExitBattle(target);
        aggroManager.Enable();
    }
}
