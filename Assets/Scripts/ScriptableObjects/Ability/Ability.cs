using UnityEngine;

public abstract class Ability : ScriptableObject
{
    public new string name;
    public float cooldown;
    public float range;
    public int damage;

    public abstract void Activate(Character caster, Character target);
}
