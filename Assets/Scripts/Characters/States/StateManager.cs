using UnityEngine;

public class StateManager : MonoBehaviour
{
    private Character character;
    private IState currentState = new OutOfBattleState();

    public void Awake()
    {
        character = GetComponent<Character>();
    }

    public void FixedUpdate()
    {
        currentState?.OnStateUpdate(character);
    }

    public void Update()
    {
        currentState?.OnStateUpdate(character);
    }
    
    public void SetState(IState newState)
    {
        if (currentState.GetType() == newState.GetType()) return;
        
        currentState?.OnStateExit(character);
        currentState = newState;
        currentState?.OnStateEnter(character);
    }
}
