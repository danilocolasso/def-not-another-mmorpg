using UnityEngine;

public interface IStatus
{
    int CurrentHealth { get; }
    int MaxHealth { get; }
    int CurrentMana { get; }
    int MaxMana { get; }
    int AttackDamage { get; }
    float AttackSpeed { get; }
    float AttackRange { get; }
    float Range { get; }
    float MoveSpeed { get; }
}
