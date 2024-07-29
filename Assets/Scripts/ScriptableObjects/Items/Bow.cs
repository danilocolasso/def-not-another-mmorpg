using UnityEngine;

[CreateAssetMenu(fileName = "NewBow", menuName = "Scriptable Objects/Items/Weapons/Bow")]
public class Bow : Weapon
{
    public ProjectileAbility projectileAbility;

    public override void Attack(Character character, Character target, Transform hand)
    {
        projectileAbility.Use(character, target, hand.position);
    }

    public override void Aim(HumanoidGraphics.Arms arms, Character target)
    {
        if (arms.Left.Transform.gameObject.activeSelf)
        {
            arms.Left.Transform.gameObject.SetActive(false);
        }
        
        Vector3 direction = target.transform.position - arms.Right.Transform.position;
        arms.Right.Transform.right = -direction;

        arms.Right.Weapon.flipX = target != null;
    }
}
