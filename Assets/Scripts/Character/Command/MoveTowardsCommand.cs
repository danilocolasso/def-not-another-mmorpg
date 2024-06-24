using UnityEngine;

public class MoveTowardsCommand : CommandInterface
{
    private readonly Rigidbody2D rb;
    private readonly Vector2 targetPosition;
    private readonly float moveSpeed;

    public MoveTowardsCommand(Rigidbody2D rb, Vector2 targetPosition, float moveSpeed)
    {
        this.rb = rb;
        this.targetPosition = targetPosition;
        this.moveSpeed = moveSpeed;
    }

    public void Execute()
    {
        Vector2 direction = (targetPosition - rb.position).normalized;
        Vector2 adjustedMovement = direction * moveSpeed * Time.fixedDeltaTime;
        Vector2 newPosition = rb.position + adjustedMovement;
        
        rb.MovePosition(newPosition);
    }
}
