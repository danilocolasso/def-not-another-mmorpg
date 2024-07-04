using UnityEngine;

public abstract class Ability : ScriptableObject
{
    public new string name;
    public float cooldown;
    public int damage;
    [Range(1, 10)] public float range;

    public abstract void Activate(Character caster, Character target);
}
