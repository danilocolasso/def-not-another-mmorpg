using UnityEngine;

[CreateAssetMenu(fileName = "NewBow", menuName = "Scriptable Objects/Items/Weapons/Bow")]
public class Bow : Weapon
{
    public ProjectileAbility projectileAbility;

    public override void Attack(Character character, Character target, Transform hand)
    {
        projectileAbility.Use(character, target, hand.position);
    }

    public override void Aim(HumanoidGraphics.Arm arm, Character target)
    {
        Vector3 direction = target.transform.position - arm.Hand.Group.transform.position;
        arm.Hand.Group.transform.right = -direction;
        // arm.Hand.Group.transform.rotation = Quaternion.LookRotation(direction, Vector3.up); // TODO test
    }

    public override void EnterBattle(HumanoidGraphics.Arm arm)
    {
        base.EnterBattle(arm);
        arm.Weapon.flipX = true;
    }

    public override void ExitBattle(HumanoidGraphics.Arm arm)
    {
        base.ExitBattle(arm);
        arm.Weapon.flipX = false;
    }
}
