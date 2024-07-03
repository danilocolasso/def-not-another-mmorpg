using UnityEngine;

public class StatusManager: MonoBehaviour, IStatus
{
    [SerializeField] private CharacterStatus baseStatus;

    public int CurrentHealth => currentHealth.Value;
    public int MaxHealth => maxHealth.Value;
    public int CurrentMana => currentMana.Value;
    public int MaxMana => maxMana.Value;
    public int AttackDamage => attackDamage.Value;
    public float AttackSpeed => attackSpeed.Value;
    public float AttackRange => attackRange.Value;
    public float Range => range.Value;
    public float MoveSpeed => moveSpeed.Value;

    private IntStat currentHealth;
    private IntStat maxHealth;
    private IntStat currentMana;
    private IntStat maxMana;
    private IntStat attackDamage;
    private FloatStat attackSpeed;
    private FloatStat attackRange;
    private FloatStat range;
    private FloatStat moveSpeed;

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

        currentHealth = new IntStat(baseStatus.CurrentHealth);
        maxHealth = new IntStat(baseStatus.MaxHealth);
        currentMana = new IntStat(baseStatus.CurrentMana);
        maxMana = new IntStat(baseStatus.MaxMana);
        attackDamage = new IntStat(baseStatus.AttackDamage);
        attackSpeed = new FloatStat(baseStatus.AttackSpeed);
        attackRange = new FloatStat(baseStatus.AttackRange);
        range = new FloatStat(baseStatus.Range);
        moveSpeed = new FloatStat(baseStatus.MoveSpeed);
    }
}
