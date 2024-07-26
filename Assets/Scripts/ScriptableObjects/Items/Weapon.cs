using UnityEngine;

public class Weapon : Equipment
{
    public enum Hand
    {
        Right,
        Left,
        Both
    }

    public int damage;
    public float attackSpeed;
    public float range;
    public float attackAngle;
    public Sprite sprite;
    public Hand hand;

    public void Attack(Character character, Character target)
    {
        // TODO implement

        // Hitbox no chest
    }
}
