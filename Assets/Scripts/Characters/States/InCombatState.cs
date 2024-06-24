using UnityEngine;

public class InCombatState : IState
{
    private Character target;

    public InCombatState(Character target)
    {
        this.target = target;
    }

    public void OnStateEnter(Character character)
    {   
        Debug.Log("[" + character + "][IState] InCombatState --> OnStateEnter");

        if (target.Target == null)
        {
            target.SetTarget(character);
        }
    }

    public void OnStateExit(Character character)
    {
        Debug.Log("[" + character + "][IState] InCombatState --> OnStateExit");
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