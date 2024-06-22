using Unity.VisualScripting;
using UnityEngine;

public class TargetMovement : MovementInterface
{
    private Transform target;
    private float stopDistance;

    public TargetMovement(Transform target, float stopDistance)
    {
        this.target = target;
        this.stopDistance = stopDistance;
    }

    public void Move(Rigidbody2D rb, float moveSpeed)
    {
        if (target == null) return;
        if (IsInRange(rb)) return;

        Vector2 targetPosition = new Vector2(target.position.x, target.position.y);
        Vector2 direction = (targetPosition - rb.position).normalized;
        Vector2 adjustedMovement = direction * moveSpeed * Time.fixedDeltaTime;
        Vector2 newPosition = rb.position + adjustedMovement;
        
        rb.MovePosition(newPosition);
    }

    public bool IsInRange(Rigidbody2D rb)
    {
        if (target == null) return false;

        return Vector2.Distance(rb.position, target.position) <= stopDistance;
    }
}
