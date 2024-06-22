using Unity.VisualScripting;
using UnityEngine;

public class TargetMovement : MovementInterface
{
    private Transform target;

    public TargetMovement(Transform target)
    {
        this.target = target;
    }

    public void Move(Rigidbody2D rb, float moveSpeed)
    {
        if (target == null) return;

        Vector2 targetPosition = new Vector2(target.position.x, target.position.y);
        Vector2 direction = (targetPosition - rb.position).normalized;
        Vector2 adjustedMovement = direction * moveSpeed * Time.fixedDeltaTime;
        Vector2 newPosition = rb.position + adjustedMovement;
        
        rb.MovePosition(newPosition);
    }
}
