using UnityEngine;

public class GraphicsManager : MonoBehaviour
{
    private bool flipped = false;

    [SerializeField] private CharacterGraphics graphics;

    public void Initialize(Character character)
    {
        Debug.Assert(character.Data.Graphics != null, $"Critical --> {character} Data.Graphics is null in the Inspector!");
        graphics.Initialize(character);
    }

    public void SetMoving(Vector2 direction, float speed = 1)
    {
        graphics.SetMoving(direction, speed);
        SetDirection(direction);
    }

    public void EnterBattle(Character target)
    {
        graphics.EnterBattle(target);
    }

    public void ExitBattle()
    {
        graphics.ExitBattle();
    }

    public virtual void SetDirection(Vector2 direction)
    {
        if (direction.x < 0 && !flipped)
        {
            graphics.Flip(true);
            flipped = true;
        }
        else if (direction.x > 0 && flipped)
        {
            graphics.Flip(false);
            flipped = false;
        }
    }

    public void Die()
    {
        graphics.SetDead();
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
