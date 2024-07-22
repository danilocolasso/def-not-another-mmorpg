public interface IStatus
{
    int CurrentHealth { get; }
    int MaxHealth { get; }
    int AttackDamage { get; }
    float AttackSpeed { get; }
    float AttackRange { get; }
    float Range { get; }
    float MoveSpeed { get; }
}
