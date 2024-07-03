using UnityEngine;

[CreateAssetMenu(fileName = "NewTargetAbility", menuName = "Scriptable Objects/Ability/Target Ability")]
public class TargetAbility : ProjectileAbility
{
    public override void Activate(Character caster, Character target)
    {
        if (projectile == null) {
          Debug.LogError("Projectile is not set in " + name + " ability.");
          return;
        }

        Projectile newProjectile = Instantiate(projectile, caster.transform.position, Quaternion.identity);
        newProjectile.Initialize(caster, target, this);
    }
}
