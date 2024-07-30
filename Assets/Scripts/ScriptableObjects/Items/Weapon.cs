using UnityEngine;
using System.Collections.Generic;

public abstract class Weapon : Equipment
{
    public enum Hand
    {
        Right,
        Left,
        Both
    }

    public float speed;
    public Sprite sprite;
    public List<Hand> hands;

    public abstract void Attack(Character character, Character target, Transform hand);
    public abstract void Aim(HumanoidArm arm, Character target);
    public virtual void EnterBattle(HumanoidArm arm) { }
    public virtual void ExitBattle(HumanoidArm arm)
    {
        arm.Reset();
    }

    public virtual void Equip(HumanoidArm arm)
    {
        arm.Weapon.sprite = sprite;
    }

    public virtual void Unequip(HumanoidArm arm)
    {
        arm.Weapon.sprite = null;
    }

    public virtual void Wield(HumanoidArm arm)
    {
        arm.Weapon.enabled = true;
    }

    public virtual void Unwield(HumanoidArm arm)
    {
        arm.Weapon.enabled = false;
    }
}
