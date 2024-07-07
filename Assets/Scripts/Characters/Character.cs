using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(StatusManager))]
[RequireComponent(typeof(StateManager))]
[RequireComponent(typeof(BattleManager))]
[RequireComponent(typeof(AbilityManager))]
[RequireComponent(typeof(MovementManager))]
public abstract class Character : MonoBehaviour
{
    [SerializeField] protected CharacterData data;
    protected StatusManager statusManager;
    protected StateManager stateManager;
    protected BattleManager battleManager;
    protected AbilityManager abilityManager;
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
        stateManager = GetComponent<StateManager>();
        battleManager = GetComponent<BattleManager>();
        abilityManager = GetComponent<AbilityManager>();
        movementManager = GetComponent<MovementManager>();

        Debug.Assert(data != null, $"{name} Character Data is null! Please assign a Character Data ScriptableObject!");
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
        Debug.Log($"{name} set target to {newTarget.name}");
        Target = newTarget;
    }

    public void OnPointerClick(BaseEventData eventData)
    {
        GameManager.Instance.Player.SetTarget(this);
    }

    public virtual void EnterBattle(Character target)
    {
        battleManager.EnterBattle(target);
    }

    public virtual void ExitBattle(Character target)
    {
        battleManager.ExitBattle(target);
    }

    public void UseAbility(string abilityName, Character target)
    {
        abilityManager.UseAbility(abilityName, this, target);
    }

    public void TakeDamage(Character attacker, int damage = 1)
    {
        Debug.Log($"{name} took {damage} damage from {attacker.name}");
        statusManager.TakeDamage(damage);

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

    public bool IsInRange(Character target, float range)
    {
        return Vector2.Distance(transform.position, target.transform.position) <= range;
    }

    public void Die()
    {
        statusManager.Die();
        battleManager.Die();
    }
}
