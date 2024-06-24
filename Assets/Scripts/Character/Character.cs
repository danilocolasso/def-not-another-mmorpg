using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CharacterData))]
[RequireComponent(typeof(StateManager))]
[RequireComponent(typeof(MovementManager))]
public abstract class Character : MonoBehaviour
{
    public Rigidbody2D Rb { get; private set; }
    public CharacterData Data { get; private set; }
    public MovementManager MovementManager { get; private set; }
    public StateManager StateManager { get; private set; }
    public Character Target { get; private set; }

    protected virtual void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
        Data = GetComponent<CharacterData>();
        StateManager = GetComponent<StateManager>();
        MovementManager = GetComponent<MovementManager>();
    }

    public void SetTarget(Character newTarget)
    {
        Target = newTarget;
    }
    
    public void SetMovement(MovementInterface newMovement)
    {
        MovementManager.SetMovement(newMovement);
    }

    public virtual void SetState(StateInterface newState)
    {
        StateManager.SetState(newState);
    }
}
