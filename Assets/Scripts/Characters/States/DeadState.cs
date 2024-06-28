using UnityEngine;

public class DeadState : IState
{
    public void OnStateEnter(Character character)
    {   
        Debug.Log("[" + character + "][IState] DeadState --> Enter");
        character.SetTarget(null);
    }

    public void OnStateExit(Character character)
    {
        Debug.Log("[" + character + "][IState] DeadState --> Exit");
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
