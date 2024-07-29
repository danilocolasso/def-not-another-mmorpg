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
    private MovementManager movementManager;
    private GraphicsManager graphicsManager;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] protected CharacterData data;

    
    public Rigidbody2D Rb { get; private set; }
    public Character Target { get; private set; }

    public bool IsAlive => statusManager.IsAlive;
    public bool IsDead => !IsAlive;
    public bool IsInBattle => battleManager.IsInBattle;
    public CharacterData Data => data;
    public StatusManager Status => statusManager;
    public EquipmentManager Equipment => equipmentManager;
    public MovementManager Movement => movementManager;
    public GraphicsManager Graphics => graphicsManager;

    public void SetMovement(IMovement newMovement)
    {
        movementManager.SetMovement(newMovement);
    }

    public void SetMoving(Vector2 direction)
    {
        graphicsManager.SetMoving(direction, Status.MoveSpeed);
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

        if (!IsInBattle)
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
