using UnityEngine;

public class StatusManager: MonoBehaviour
{
    [SerializeField] private CharacterStatus baseStatus;

    public Stat CurrentHealth { get; private set; }
    public Stat MaxHealth { get; private set; }
    public Stat CurrentMana { get; private set; }
    public Stat MaxMana { get; private set; }
    public Stat AttackDamage { get; private set; }
    public Stat AttackSpeed { get; private set; }
    public Stat Range { get; private set; }
    public Stat MoveSpeed { get; private set; }

    protected void Awake()
    {
        SetBaseStatus();
    }

    public void TakeDamage(float amount)
    {
        CurrentHealth.Decrement(amount);
    }

    public void Heal(float amount)
    {
        CurrentHealth.Increment(amount);
    }

    public void UseMana(float amount)
    {
        CurrentMana.Decrement(amount);
    }

    public void RestoreMana(float amount)
    {
        CurrentMana.Increment(amount);
    }

    private void SetBaseStatus()
    {
        CurrentHealth = new Stat(baseStatus.CurrentHealth);
        MaxHealth = new Stat(baseStatus.MaxHealth);
        CurrentMana = new Stat(baseStatus.CurrentMana);
        MaxMana = new Stat(baseStatus.MaxMana);
        AttackDamage = new Stat(baseStatus.AttackDamage);
        AttackSpeed = new Stat(baseStatus.AttackSpeed);
        Range = new Stat(baseStatus.Range);
        MoveSpeed = new Stat(baseStatus.MoveSpeed);
    }
}
