using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Scriptable Objects/Character/Data")]
public class CharacterData : ScriptableObject
{
    [SerializeField] private int level = 1;
    [SerializeField] private float health = 100f;
    [SerializeField] private float mana = 0f;
    [SerializeField] private float attackDamage = 1f;
    [SerializeField] private float attackSpeed = 1f;
    [SerializeField] private DamageType attackDamageType = DamageType.Physical;
    [Range(0, 10)][SerializeField] private float range = 1f;
    [Range(0, 15)][SerializeField] private float moveSpeed = 5f;

    public int Level => level;
    public float Health => health;
    public float Mana => mana;
    public float AttackDamage => attackDamage;
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
