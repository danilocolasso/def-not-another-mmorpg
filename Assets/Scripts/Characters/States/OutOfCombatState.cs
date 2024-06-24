using UnityEngine;

public class OutOfCombatState : IState
{
    public void OnStateEnter(Character character)
    {
        Debug.Log("[" + character + "][IState] OutOfCombatState --> OnStateEnter");
    }

    public void OnStateExit(Character character)
    {
        Debug.Log("[" + character + "][IState] OutOfCombatState --> OnStateExit");
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
