using UnityEngine;

public class OutOfBattleState : IState
{
    public void OnStateEnter(Character character)
    {
        Debug.Log("[" + character + "][IState] OutOfBattleState --> OnStateEnter");
    }

    public void OnStateExit(Character character)
    {
        Debug.Log("[" + character + "][IState] OutOfBattleState --> OnStateExit");
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
