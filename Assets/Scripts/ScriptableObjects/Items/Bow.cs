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
        arm.Hand.Group.transform.right = -direction;
        // arm.Hand.Group.transform.rotation = Quaternion.LookRotation(direction, Vector3.up); // TODO test
    }

    public override void EnterBattle(HumanoidArm arm)
    {
        base.EnterBattle(arm);
        arm.Hand.Weapon.flipX = true;
    }

    public override void ExitBattle(HumanoidArm arm)
    {
        base.ExitBattle(arm);
        arm.Hand.Weapon.flipX = false;
    }
}
