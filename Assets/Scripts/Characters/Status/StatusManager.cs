using UnityEngine;

public class StatusManager: MonoBehaviour, IStatus
{
    [SerializeField] private CharacterStatus baseStatus;

    private IntStat currentHealth;
    private IntStat maxHealth;
    private IntStat attackDamage;
    private FloatStat attackSpeed;
    private FloatStat attackRange;
    private FloatStat range;
    private FloatStat moveSpeed;

    public int CurrentHealth => currentHealth.Value;
    public int MaxHealth => maxHealth.Value;
    public int AttackDamage => attackDamage.Value;
    public float AttackSpeed => attackSpeed.Value;
    public float AttackRange => attackRange.Value;
    public float Range => range.Value;
    public float MoveSpeed => moveSpeed.Value;
    public bool IsAlive => currentHealth.Value > 0;

    protected void Awake()
    {
        SetBaseStatus();
    }

    public void TakeDamage(int damage)
    {
        currentHealth.Decrement(damage);
    }

    public void Heal(int amount)
    {
        currentHealth.Increment(amount);
    }

    public void Die()
    {
        currentHealth.SetValue(0);
    }

    private void SetBaseStatus()
    {
        Debug.Assert(baseStatus != null, "Base Status is null! Please assign a Character Status ScriptableObject!");

        currentHealth = new IntStat(baseStatus.Health);
        maxHealth = new IntStat(baseStatus.Health);
        attackDamage = new IntStat(baseStatus.AttackDamage);
        attackSpeed = new FloatStat(baseStatus.AttackSpeed);
        attackRange = new FloatStat(baseStatus.AttackRange);
        range = new FloatStat(baseStatus.Range);
        moveSpeed = new FloatStat(baseStatus.MoveSpeed);
    }
}
