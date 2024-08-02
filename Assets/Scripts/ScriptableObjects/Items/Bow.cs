using UnityEngine;

[CreateAssetMenu(fileName = "NewBow", menuName = "Scriptable Objects/Items/Weapons/Bow")]
public class Bow : Weapon
{
    public ProjectileAbility projectileAbility;

    public override void Attack(Character character, Character target, Transform hand)
    {
        projectileAbility.Use(character, target, hand.position);
    }

    public override void Aim(HumanoidArm arm, Character target)
    {
        Vector3 direction = target.transform.position - arm.Hand.Group.transform.position;
        arm.Group.transform.parent.right = direction;
    }
}
