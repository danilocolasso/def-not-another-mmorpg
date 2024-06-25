using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(StatusManager))]
[RequireComponent(typeof(StateManager))]
[RequireComponent(typeof(MovementManager))]
public abstract class Character : MonoBehaviour
{   
    public Rigidbody2D Rb { get; private set; }
    public StatusManager StatusManager { get; private set; }
    public MovementManager MovementManager { get; private set; }
    public StateManager StateManager { get; private set; }
    public Character Target { get; private set; }
    
    protected virtual void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
        StatusManager = GetComponent<StatusManager>();
        StateManager = GetComponent<StateManager>();
        MovementManager = GetComponent<MovementManager>();
    }
    
    public void SetMovement(IMovement newMovement)
    {
        MovementManager.SetMovement(newMovement);
    }

    public virtual void SetState(IState newState)
    {
        StateManager.SetState(newState);
    }

    public void SetTarget(Character newTarget)
    {
        Target = newTarget;
    }
}
