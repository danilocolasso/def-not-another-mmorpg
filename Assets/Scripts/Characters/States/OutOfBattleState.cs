using UnityEngine;

public class OutOfBattleState : IState
{
    public void OnStateEnter(Character character)
    {
        Debug.Log("[" + character + "][IState] OutOfBattleState --> Enter");
    }

    public void OnStateExit(Character character)
    {
        Debug.Log("[" + character + "][IState] OutOfBattleState --> Exit");
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
