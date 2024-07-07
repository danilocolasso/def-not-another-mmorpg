using UnityEngine;

public class InBattleState : IState
{
    private readonly Character target;

    public InBattleState(Character target)
    {
        this.target = target;
    }

    public void OnStateEnter(Character character)
    {
        if (character.Target == null)
        {
            character.SetTarget(target);
        }
    }

    public void OnStateExit(Character character)
    {
        // Do nothing
    }

    public void OnStateFixedUpdate(Character character)
    {
        // Do nothing
    }

    public void OnStateUpdate(Character character)
    {
        // Do nothing
    }
}