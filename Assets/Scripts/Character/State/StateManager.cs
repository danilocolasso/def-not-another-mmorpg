using UnityEngine;

public class StateManager : MonoBehaviour
{
    private Character character;
    private StateInterface currentState = new OutOfCombatState();
    public StateInterface CurrentState => currentState;

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
    
    public void SetState(StateInterface newState)
    {
        if (currentState == newState) return;
        
        currentState?.OnStateExit(character);
        currentState = newState;
        currentState?.OnStateEnter(character);
    }
}
