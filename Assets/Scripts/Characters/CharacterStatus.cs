using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Scriptable Objects/Character/Data")]
public class CharacterStatus : ScriptableObject
{
    [SerializeField] private int currentHealth = 100;
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int currentMana = 100;
    [SerializeField] private int maxMana = 100;
    [SerializeField] private float attackSpeed = 1f;
    [SerializeField] private int attackDamage = 1;
    [Range(0, 10)][SerializeField] private float attackRange = 1f;
    [Range(0, 10)][SerializeField] private float range = 1f;
    [Range(0, 15)][SerializeField] private float moveSpeed = 5;

    public int CurrentHealth => currentHealth;
    public int MaxHealth => maxHealth;
    public int CurrentMana => currentMana;
    public int MaxMana => maxMana;
    public float AttackSpeed => attackSpeed;
    public int AttackDamage => attackDamage;
    public float AttackRange => attackRange;
    public float Range => range;
    public float MoveSpeed => moveSpeed;
}
