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
    public abstract void Aim(HumanoidGraphics.Arm arm, Character target);
    public virtual void EnterBattle(HumanoidGraphics.Arm arm) { }
    public virtual void ExitBattle(HumanoidGraphics.Arm arm)
    {
        arm.Reset();
    }

    public virtual void Equip(HumanoidGraphics.Arm arm)
    {
        arm.Weapon.sprite = sprite;
    }

    public virtual void Unequip(HumanoidGraphics.Arm arm)
    {
        arm.Weapon.sprite = null;
    }

    public virtual void Wield(HumanoidGraphics.Arm arm)
    {
        arm.Weapon.enabled = true;
    }

    public virtual void Unwield(HumanoidGraphics.Arm arm)
    {
        arm.Weapon.enabled = false;
    }
}
