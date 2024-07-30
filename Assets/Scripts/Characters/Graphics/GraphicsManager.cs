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

    public void SetMoving(Vector2 direction, float speed)
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

    public void Aim(Weapon weapon, Weapon.Hand hand, Character target)
    {
        if (target != null)
        {
            // graphics.Aim(weapon, hand, target);
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
