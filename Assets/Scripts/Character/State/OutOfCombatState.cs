using UnityEngine;

public class OutOfCombatState : StateInterface
{
    public void OnStateEnter(Character character)
    {
        Debug.Log("[" + character + "][StateInterface] OutOfCombatState --> OnStateEnter");
    }

    public void OnStateExit(Character character)
    {
        Debug.Log("[" + character + "][StateInterface] OutOfCombatState --> OnStateExit");
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
