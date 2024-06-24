using UnityEngine;

public class Enemy : Character
{
    public override void SetState(StateInterface state)
    {
        base.SetState(state);

        if (state is InCombatState)
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
        SetMovement(new FollowTargetMovement(Target.transform, Data.Status.AttackRange));
    }

    private void StopChaseTarget()
    {
        SetMovement(null);
    }
}
