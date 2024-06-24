using UnityEngine;

[CreateAssetMenu(fileName = "CharacterStatus", menuName = "Scriptable Objects/Character/Status")]
public class CharacterStatus : ScriptableObject
{
    [SerializeField] private Stat health = new Stat { currentValue = 100, maxValue = 100 };
    [SerializeField] private Stat mana = new Stat { currentValue = 100, maxValue = 100 };
    [SerializeField] private float attackDamage = 1f;
    [SerializeField] private float attackSpeed = 1f;
    [SerializeField] private DamageType attackDamageType = DamageType.Physical;
    [Range(0, 10)][SerializeField] private float range = 1f;
    [Range(0, 10)][SerializeField] private float moveSpeed = 5f;

    public Stat Health => health;
    public Stat Mana => mana;
    public float Range => range;
    public float MoveSpeed => moveSpeed;

    [System.Serializable]
    public struct Stat
    {
        public float currentValue;
        public float maxValue;
    }

    public enum DamageType
    {
        Physical,
        Magical,
    }
}
