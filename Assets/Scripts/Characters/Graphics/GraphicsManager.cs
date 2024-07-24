using UnityEngine;

public class GraphicsManager : MonoBehaviour
{
    private bool flipped = false;

    [SerializeField] private CharacterBodyGraphics graphics;

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

    public void SetInBattle(bool isInBattle)
    {
        graphics.SetInBattle(isInBattle);
    }

    public virtual void SetDirection(Vector2 direction)
    {
        if (direction.x < 0 && !flipped)
        {
            Flip(true);
            return;
        }

        if (direction.x > 0 && flipped)
        {
            Flip(false);
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
            graphics = GetComponentInChildren<CharacterBodyGraphics>();
            Debug.LogWarning($"Performance --> Assign {name} Graphics child object to GraphicsManager in the Inspector!");
        }
    }

    private void Flip(bool flip)
    {
        flipped = flip;
        graphics.transform.Rotate(0, flip ? 180 : -180, 0);
    }
}
