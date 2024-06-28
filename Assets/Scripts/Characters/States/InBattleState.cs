using UnityEngine;

public class InBattleState : IState
{
    private Character target;

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

        Debug.Log("[" + character + "][IState] InBattleState --> Enter");
    }

    public void OnStateExit(Character character)
    {
        Debug.Log("[" + character + "][IState] InBattleState --> Exit");
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