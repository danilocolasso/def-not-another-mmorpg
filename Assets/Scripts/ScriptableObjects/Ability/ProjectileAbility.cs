using UnityEngine;

[CreateAssetMenu(fileName = "NewProjectileAbility", menuName = "Scriptable Objects/Ability/Projectile")]
public class ProjectileAbility : Ability
{
    [Range(1, 20)] public float speed;
    public Projectile projectile;
    
    public override void Use(Character caster, Character target)
    {
        if (projectile == null) {
          Debug.Log("Projectile is not set in " + name + " ability.");
          return;
        }

        Projectile newProjectile = Instantiate(projectile, caster.transform.position, Quaternion.identity);
        newProjectile.Initialize(caster, target, this);
    }
}
