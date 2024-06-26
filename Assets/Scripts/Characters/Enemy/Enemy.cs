using UnityEngine;

public class Enemy : Character
{
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
        SetMovement(new TargetMovement(Target.transform, StatusManager.Range.Value));
    }

    private void StopChaseTarget()
    {
        SetMovement(null);
    }
}
