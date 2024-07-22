using UnityEngine;

public class InputMovement : IMovement
{
    private InputHandler inputHandler;

    public InputMovement(InputHandler inputHandler)
    {
        this.inputHandler = inputHandler;
    }

    public Vector2 Move(Rigidbody2D rb, float moveSpeed)
    {
        if (inputHandler == null)
        {
            return Vector2.zero;
        }

        Vector2 movement = inputHandler.MovementInput.normalized;
        Vector2 targetPosition = rb.position + movement;
        ICommand command = new MoveTowardsCommand(rb, targetPosition, moveSpeed);
        command.Execute();

        return movement;
    }
}
