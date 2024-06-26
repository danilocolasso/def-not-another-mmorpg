using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(StatusManager))]
[RequireComponent(typeof(StateManager))]
[RequireComponent(typeof(BattleManager))]
[RequireComponent(typeof(MovementManager))]
public abstract class Character : MonoBehaviour
{
    public Rigidbody2D Rb { get; private set; }
    public StatusManager StatusManager { get; private set; }
    public StateManager StateManager { get; private set; }
    public BattleManager BattleManager { get; private set; }
    public MovementManager MovementManager { get; private set; }
    public Character Target { get; private set; }

    public bool IsAlive => StatusManager.IsAlive;
    public bool IsDead => !IsAlive;

    protected virtual void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
        StatusManager = GetComponent<StatusManager>();
        MovementManager = GetComponent<MovementManager>();
        StateManager = GetComponent<StateManager>();
        BattleManager = GetComponent<BattleManager>();
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

    public void EnterBattle(Character target)
    {
        BattleManager.EnterBattle(target);
    }

    public void ExitBattle(Character target)
    {
        BattleManager.ExitBattle(target);
    }

    public void Hit(int amount)
    {
        StatusManager.TakeDamage(amount);

        if (IsDead)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        StatusManager.Heal(amount);
    }

    public void Die()
    {
        StatusManager.Die();
        BattleManager.Die();
    }
}
