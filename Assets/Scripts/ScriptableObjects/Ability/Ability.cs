using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : ScriptableObject
{
    public new string name;
    public float cooldown;
    [Range(1, 10)] public float range;
    public List<AbilityEffect> effects;

    public virtual bool CanUse(Character caster, Character target) => target.IsAlive;
    public abstract void Use(Character caster, Character target);

    public void ApplyEffect(Character caster, Character target)
    {
        foreach (AbilityEffect effect in effects)
        {
            if (effect.CanApply(caster, target, this))
            {
                effect.Apply(caster, target, this);
            }
        }
    }
}
