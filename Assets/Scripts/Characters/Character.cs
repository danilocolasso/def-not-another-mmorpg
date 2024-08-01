using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(GraphicsManager))]
[RequireComponent(typeof(StatusManager))]
[RequireComponent(typeof(ExperienceManager))]
[RequireComponent(typeof(StateManager))]
[RequireComponent(typeof(BattleManager))]
[RequireComponent(typeof(EquipmentManager))]
[RequireComponent(typeof(AbilityManager))]
[RequireComponent(typeof(MovementManager))]
public abstract class Character : MonoBehaviour
{
    private StatusManager statusManager;
    private ExperienceManager experienceManager;
    private StateManager stateManager;
    private BattleManager battleManager;
    private AbilityManager abilityManager;
    private EquipmentManager equipmentManager;
    private GraphicsManager graphicsManager;
    protected MovementManager movementManager;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] protected CharacterData data;

    
    public Rigidbody2D Rb { get; private set; }
    public Character Target { get; private set; }

    public bool IsAlive => statusManager.IsAlive;
    public bool IsDead => !IsAlive;
    public bool IsInBattle => battleManager.IsInBattle;
    public bool IsOutOfBattle => !IsInBattle;
    public CharacterData Data => data;
    public StatusManager Status => statusManager;

    public void SetMovement(IMovement newMovement)
    {
        movementManager.SetMovement(newMovement);
    }

    public void SetMoving(Vector2 direction, float speed)
    {
        graphicsManager.SetMoving(direction, speed);
    }

    public virtual void SetState(IState newState)
    {
        stateManager.SetState(newState);
    }

    public void SetTarget(Character newTarget)
    {
        Debug.Log($"{name} set target to {(newTarget == null ? "null" : newTarget)}");
    
        Target = newTarget;
        graphicsManager.SetTarget(newTarget);
    }

    public void OnPointerClick(BaseEventData _)
    {
        GameManager.Instance.Player.SetTarget(this);
    }

    public virtual void EnterBattle(Character target)
    {
        if (!battleManager.IsInBattleWith(target))
        {
            battleManager.EnterBattle(target);
            graphicsManager.EnterBattle(target);
        }
    }

    public virtual void ExitBattle(Character target)
    {
        if (battleManager.IsInBattleWith(target))
        {
            battleManager.ExitBattle(target);
        }

        if (IsOutOfBattle)
        {
            graphicsManager.ExitBattle();
        }
    }

    public bool UseAbility(string abilityName, Character target)
    {
        return abilityManager.UseAbility(abilityName, target);
    }

    public void Attack(Character target)
    {
        battleManager.Attack(target);
        graphicsManager.Attack(target);
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

    public void GainExperience(int amount)
    {
        experienceManager.GainExperience(amount);
    }

    public bool IsInRange(Character target, float range)
    {
        return Vector2.Distance(transform.position, target.transform.position) <= range;
    }

    public void Aim(Character target)
    {
        equipmentManager.Aim(target);
    }

    public void Wield()
    {
        equipmentManager.Wield();
    }

    public void Unwield()
    {
        equipmentManager.Unwield();
    }

    public void Equip(Weapon weapon, Weapon.Hand hand = Weapon.Hand.Right)
    {
        equipmentManager.Equip(weapon, hand == Weapon.Hand.Left ? equipmentManager.Arms.Left : equipmentManager.Arms.Right);
    }

    public void Die()
    {
        statusManager.Die();
        battleManager.Die();
        graphicsManager.Die();
    }

    protected virtual void Awake()
    {
        Debug.Assert(data != null, $"Critical --> Assign a Data to {name} in the Inspector!");

        Rb = GetComponent<Rigidbody2D>();
        statusManager = GetComponent<StatusManager>();
        experienceManager = GetComponent<ExperienceManager>();
        stateManager = GetComponent<StateManager>();
        battleManager = GetComponent<BattleManager>();
        abilityManager = GetComponent<AbilityManager>();
        equipmentManager = GetComponent<EquipmentManager>();
        movementManager = GetComponent<MovementManager>();
        graphicsManager = GetComponentInChildren<GraphicsManager>();

        statusManager.Initialize(this);
        experienceManager.Initialize(this);
        stateManager.Initialize(this);
        battleManager.Initialize(this);
        abilityManager.Initialize(this);
        equipmentManager.Initialize(this);
        movementManager.Initialize(this);
        graphicsManager.Initialize(this);
    }
}
