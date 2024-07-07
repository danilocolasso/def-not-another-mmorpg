
using UnityEngine;

public abstract class AbilityEffect: ScriptableObject
{
    public virtual bool CanApply(Character caster, Character target, Ability ability) => target.IsAlive;
    public abstract void Apply(Character caster, Character target, Ability ability);
}
