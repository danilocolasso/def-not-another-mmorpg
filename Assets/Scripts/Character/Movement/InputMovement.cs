using UnityEngine;
using UnityEngine.InputSystem;

public class InputMovement : MovementInterface
{
    private InputHandler inputHandler;

    public InputMovement(InputHandler inputHandler)
    {
        this.inputHandler = inputHandler;
    }

    public void Move(Rigidbody2D rb, float moveSpeed)
    {
        if (inputHandler == null) return;

        Vector2 targetPosition = rb.position + inputHandler.MovementInput.normalized;
        CommandInterface movement = new MoveTowardsCommand(rb, targetPosition, moveSpeed);
        movement.Execute();
    }
}
