using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    private Character caster;
    private Character target;
    private ProjectileAbility ability;
    private Rigidbody2D rb;

    public void Initialize(Character caster, Character target, ProjectileAbility ability)
    {
        this.caster = caster;
        this.target = target;
        this.ability = ability;

        rb = GetComponent<Rigidbody2D>();

        transform.SetParent(caster.transform);
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Hitbox hitbox) && hitbox.Character == target)
        {
            target.TakeDamage(caster, ability.damage);
            Destroy(gameObject);
        }
    }

    protected void Update()
    {
        Debug.Log("target: " + target + " caster: " + caster + " ability: " + ability);

        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        ICommand moveCommand = new MoveTowardsCommand(rb, target.transform.position, ability.speed);
        moveCommand.Execute();
    }
}
