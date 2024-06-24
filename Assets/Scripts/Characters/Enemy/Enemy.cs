using UnityEngine;

public class Enemy : Character
{
    public override void SetState(IState state)
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
        SetMovement(new FollowTargetMovement(Target.transform, Data.Status.Range));
    }

    private void StopChaseTarget()
    {
        SetMovement(null);
    }
}
