using System.Linq;
using UnityEngine;

public class GraphicsManager : MonoBehaviour
{
    [SerializeField] private CharacterGraphics graphics;

    public void Initialize(Character character)
    {
        Debug.Assert(character.Data.Graphics != null, $"Critical --> Assign a Graphics to {character} Data in the Inspector!");
        graphics.Initialize(character);
    }

    public void SetTarget(Character target)
    {
        graphics.SetTarget(target);
    }

    public void SetMoving(Vector2 direction, float speed = 1)
    {
        graphics.SetMoving(direction, speed);
        graphics.SetDirection(direction);
    }

    public void EnterBattle(Character target)
    {
        graphics.EnterBattle(target);
    }

    public void ExitBattle()
    {
        graphics.ExitBattle();
    }

    public void Equip(Weapon weapon)
    {
        Equip(weapon, weapon.hands.First());
    }

    public void Equip(Weapon weapon, Weapon.Hand hand)
    {
        graphics.Equip(weapon, hand);
    }

    public void Unequip(Weapon.Hand hand)
    {
        graphics.Unequip(hand);
    }

    public void Aim(Character target)
    {
        if (target != null)
        {
            graphics.Aim(target);
        }
    }

    public void Die()
    {
        graphics.Die();
    }

    private void Awake()
    {
        if (graphics == null)
        {
            graphics = GetComponentInChildren<CharacterGraphics>();
            Debug.LogWarning($"Performance --> Assign {name} Graphics child object to GraphicsManager in the Inspector!");
        }
    }
}
