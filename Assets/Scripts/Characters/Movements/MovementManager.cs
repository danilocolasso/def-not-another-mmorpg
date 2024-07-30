using UnityEngine;

public class MovementManager : MonoBehaviour
{
    private Character character;
    private IMovement movement;

    public void Initialize(Character character)
    {
        this.character = character;
    }

    public void FixedUpdate()
    {
        if (movement != null)
        {
            Vector2 direction = movement.Move(character.Rb, character.Status.MoveSpeed);
            character.SetMoving(direction, character.Status.MoveSpeed);
        }
    }

    public void SetMovement(IMovement newMovement)
    {
        movement = newMovement;
    }
}
