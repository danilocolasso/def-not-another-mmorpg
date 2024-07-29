using UnityEngine;

public class StateManager : MonoBehaviour
{
    private Character character;
    private IState currentState = new OutOfBattleState();

    public void Initialize(Character character)
    {
        this.character = character;
    }
    
    public void SetState(IState newState)
    {
        if (currentState.GetType() == newState.GetType()) return;
        
        currentState?.OnStateExit(character);
        currentState = newState;
        currentState?.OnStateEnter(character);
    }

    private void FixedUpdate()
    {
        currentState?.OnStateFixedUpdate(character);
    }

    private void Update()
    {
        currentState?.OnStateUpdate(character);
    }
}
