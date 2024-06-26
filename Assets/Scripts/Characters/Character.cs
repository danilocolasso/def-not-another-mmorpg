using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(StatusManager))]
[RequireComponent(typeof(StateManager))]
[RequireComponent(typeof(BattleManager))]
[RequireComponent(typeof(MovementManager))]
public abstract class Character : MonoBehaviour
{
    [SerializeField] protected CharacterData data;
    protected StatusManager statusManager;
    protected StateManager stateManager;
    protected BattleManager battleManager;
    protected MovementManager movementManager;
    public Rigidbody2D Rb { get; private set; }
    public Character Target { get; private set; }

    public bool IsAlive => statusManager.IsAlive;
    public bool IsDead => !IsAlive;
    public bool IsInBattle => battleManager.IsInBattle;
    public CharacterData Data => data;
    public IStatus Status => statusManager;

    protected virtual void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
        statusManager = GetComponent<StatusManager>();
        movementManager = GetComponent<MovementManager>();
        stateManager = GetComponent<StateManager>();
        battleManager = GetComponent<BattleManager>();

        Debug.Assert(data != null, "Character Data is null! Please assign a Character Data ScriptableObject!");
    }

    public void SetMovement(IMovement newMovement)
    {
        movementManager.SetMovement(newMovement);
    }

    public virtual void SetState(IState newState)
    {
        stateManager.SetState(newState);
    }

    public void SetTarget(Character newTarget)
    {
        Target = newTarget;
    }

    public virtual void EnterBattle(Character target)
    {
        battleManager.EnterBattle(target);
    }

    public virtual void ExitBattle(Character target)
    {
        battleManager.ExitBattle(target);
    }

    public void TakeDamage(Character attacker)
    {
        statusManager.TakeDamage(attacker);

        if (IsDead)
        {
            Die();
            return;
        }

        EnterBattle(attacker);
    }

    public void Heal(int amount)
    {
        statusManager.Heal(amount);
    }

    public bool IsInRange(Character target)
    {
        return Vector2.Distance(transform.position, target.transform.position) <= Status.Range;
    }

    public void Die()
    {
        statusManager.Die();
        battleManager.Die();
    }
}
