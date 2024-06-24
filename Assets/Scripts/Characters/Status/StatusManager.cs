using UnityEngine;

public class StatusManager: MonoBehaviour
{
    [SerializeField] private CharacterData characterData;
    private Status health;
    private Status mana;
    public int Level { get; private set; }
    public Status AttackDamage { get; private set; }
    public Status AttackSpeed { get; private set; }
    public CharacterData.DamageType AttackDamageType { get; private set; }
    public Status Range { get; private set; }
    public Status MoveSpeed { get; private set; }

    public Status Health => health;
    public Status Mana => mana;

    protected void Awake()
    {
        if (characterData == null) {
            Character character = GetComponent<Character>();
            throw new System.Exception("Character [" + character + "] Data is not set");
        }

        SetBaseValues();
    }

    public void LevelUp()
    {
        Level++;
    }

    public void TakeDamage(float amount)
    {
        health.Decrement(amount);
    }

    public void Heal(float amount)
    {
        health.Increment(amount);
    }

    public void UseMana(float amount)
    {
        mana.Decrement(amount);
    }

    public void RestoreMana(float amount)
    {
        mana.Increment(amount);
    }

    private void SetBaseValues()
    {
        health = new Status(characterData.Health);
        mana = new Status(characterData.Mana);
        Level = characterData.Level;
        AttackDamage = new Status(characterData.AttackDamage);
        AttackSpeed = new Status(characterData.AttackSpeed);
        AttackDamageType = characterData.AttackDamageType;
        Range = new Status(characterData.Range);
        MoveSpeed = new Status(characterData.MoveSpeed);
    }

    [System.Serializable]
    public struct Stat
    {
        public float currentValue;
        public float maxValue;
    }
}
