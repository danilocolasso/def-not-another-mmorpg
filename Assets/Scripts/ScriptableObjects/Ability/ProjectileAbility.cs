using UnityEngine;

public abstract class ProjectileAbility : Ability
{
    [Range(1, 10)] public float speed;
    public Projectile projectile;
}
