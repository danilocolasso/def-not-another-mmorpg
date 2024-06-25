using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Scriptable Objects/Character/Data")]
public class CharacterStatus : ScriptableObject
{
    [SerializeField] private int currentHealth = 100;
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int currentMana = 0;
    [SerializeField] private int maxMana = 0;
    [SerializeField] private float attackSpeed = 1f;
    [SerializeField] private int attackDamage = 1;
    [SerializeField] private DamageType attackDamageType = DamageType.Physical;
    [Range(0, 10)][SerializeField] private float range = 1;
    [Range(0, 15)][SerializeField] private float moveSpeed = 5;

    public int CurrentHealth => currentHealth;
    public int MaxHealth => maxHealth;
    public int CurrentMana => currentMana;
    public int MaxMana => maxMana;
    public int AttackDamage => attackDamage;
    public float AttackSpeed => attackSpeed;
    public DamageType AttackDamageType => attackDamageType;
    public float Range => range;
    public float MoveSpeed => moveSpeed;

    public enum DamageType
    {
        Physical,
        Magical,
    }
}
