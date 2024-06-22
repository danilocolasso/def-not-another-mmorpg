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

        Vector2 input = inputHandler.MovementInput;
        Vector2 movement = input.normalized * moveSpeed * Time.fixedDeltaTime;

        rb.MovePosition(rb.position + movement);
    }
}
