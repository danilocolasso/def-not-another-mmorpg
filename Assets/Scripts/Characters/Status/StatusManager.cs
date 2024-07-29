using UnityEngine;

public class StatusManager: MonoBehaviour
{
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
    private Character character;

    public void Initialize(Character character)
    {
        this.character = character;
        SetBaseStatus(character.Data.Status);
    }

    public void TakeDamage(int damage)
    {
        currentHealth.Decrement(damage);
    }

    public void Die()
    {
        currentHealth.SetValue(0);
    }

    private void SetBaseStatus(CharacterStatus status)
    {
        Debug.Assert(status != null, $"Critical --> Assign a Status to {character} Data in the Inspector!");

        currentHealth = new IntStat(status.Health);
        maxHealth = new IntStat(status.Health);
        attackDamage = new IntStat(status.AttackDamage);
        attackSpeed = new FloatStat(status.AttackSpeed);
        attackRange = new FloatStat(status.AttackRange);
        range = new FloatStat(status.Range);
        moveSpeed = new FloatStat(status.MoveSpeed);
    }
}
