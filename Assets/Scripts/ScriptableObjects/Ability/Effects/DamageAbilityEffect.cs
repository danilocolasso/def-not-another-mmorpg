using UnityEngine;

[CreateAssetMenu(fileName = "NewDamageAbilityEffect", menuName = "Scriptable Objects/Ability/Effects/Damage")]
public class DamageAbilityEffect : AbilityEffect
{
    public int damage;

    public override void Apply(Character caster, Character target, Ability ability)
    {
        target.TakeDamage(caster, damage);
    }
}
