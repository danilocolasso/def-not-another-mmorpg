using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacterStatus", menuName = "Scriptable Objects/Character/Status")]
public class CharacterStatus : ScriptableObject
{
    [SerializeField] private int health = 100;
    [SerializeField] private float attackSpeed = 1f;
    [SerializeField] private int attackDamage = 1;
    [Range(0, 5)][SerializeField] private float attackRange = 1f;
    [Range(0, 10)][SerializeField] private float range = 1f;
    [Range(0, 5)][SerializeField] private float moveSpeed = 5;

    public int Health => health;
    public float AttackSpeed => attackSpeed;
    public int AttackDamage => attackDamage;
    public float AttackRange => attackRange;
    public float Range => range;
    public float MoveSpeed => moveSpeed;
}
