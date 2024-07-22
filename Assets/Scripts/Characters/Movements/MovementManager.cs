using UnityEngine;

public class MovementManager : MonoBehaviour
{
    private Character character;
    private IMovement movement;

    private void Awake()
    {
        character = GetComponent<Character>();
    }

    public void FixedUpdate()
    {
        if (movement == null)
        {
            return;
        }

        Vector2 direction = movement.Move(character.Rb, character.Status.MoveSpeed);
        character.SetIsMoving(direction);
    }

    public void SetMovement(IMovement newMovement)
    {
        movement = newMovement;
    }
}
