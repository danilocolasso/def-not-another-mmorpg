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

    private void OnTriggerEnter2D(Collider2D other)
    {
        Character target = other.GetComponent<Character>();
        ICommand command = new DetectCharacterCommand(this, target);
        command.Execute();
    }

    private void StartChaseTarget()
    {
        SetMovement(new TargetMovement(Target.transform, Status.Range));
    }

    private void StopChaseTarget()
    {
        SetMovement(null);
    }
}
