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
    public Stat AttackRange { get; private set; }
    public Stat Range { get; private set; }
    public Stat MoveSpeed { get; private set; }

    public bool IsAlive => CurrentHealth.Value > 0;

    protected void Awake()
    {
        SetBaseStatus();
    }

    public void TakeDamage(int amount)
    {
        CurrentHealth.Decrement(amount);
        Debug.Log($"{name} ---> {CurrentHealth.Value}/{MaxHealth.Value}");
    }

    public void Heal(int amount)
    {
        CurrentHealth.Increment(amount);
    }

    public void UseMana(int amount)
    {
        CurrentMana.Decrement(amount);
    }

    public void RestoreMana(int amount)
    {
        CurrentMana.Increment(amount);
    }

    public void Die()
    {
        CurrentHealth.SetValue(0);
    }

    private void SetBaseStatus()
    {
        CurrentHealth = new Stat(baseStatus.CurrentHealth);
        MaxHealth = new Stat(baseStatus.MaxHealth);
        CurrentMana = new Stat(baseStatus.CurrentMana);
        MaxMana = new Stat(baseStatus.MaxMana);
        AttackDamage = new Stat(baseStatus.AttackDamage);
        AttackSpeed = new Stat(baseStatus.AttackSpeed);
        AttackRange = new Stat(baseStatus.AttackRange);
        Range = new Stat(baseStatus.Range);
        MoveSpeed = new Stat(baseStatus.MoveSpeed);
    }
}
