using UnityEngine;

[CreateAssetMenu(fileName = "NewProjectileAbility", menuName = "Scriptable Objects/Ability/Projectile")]
public class ProjectileAbility : Ability
{
    [Range(1, 20)] public float speed;
    public Projectile projectile;
    
    public override void Use(Character character, Character target)
    {
        Use(character, target, character.transform.position);
    }

    public void Use(Character character, Character target, Vector3 spanwPosition)
    {
        Debug.Assert(projectile != null, $"Critical --> Assign a Projectile to {name} in the Inspector!");

        Projectile newProjectile = Instantiate(projectile, spanwPosition, Quaternion.identity);
        newProjectile.Initialize(character, target, this);
    }
}
