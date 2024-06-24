using UnityEngine;

public class FollowTargetMovement : IMovement
{
    private readonly Transform target;
    private readonly float stopDistance;

    public FollowTargetMovement(Transform target, float stopDistance = 1f)
    {
        this.target = target;
        this.stopDistance = stopDistance;
    }

    public void Move(Rigidbody2D rb, float moveSpeed)
    {
        if (target == null) return;
        if (IsInRange(rb.position, target.position)) return;

        ICommand movement = new MoveTowardsCommand(rb, target.position, moveSpeed);
        movement.Execute();
    }

    private bool IsInRange(Vector2 currentPosition, Vector2 targetPosition)
    {
        return Vector2.Distance(currentPosition, targetPosition) <= stopDistance;
    }
}
