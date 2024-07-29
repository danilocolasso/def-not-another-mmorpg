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
    public abstract void Aim(HumanoidGraphics.Arms arms, Character target);
}
