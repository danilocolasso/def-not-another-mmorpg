using UnityEngine;

public class TargetMovement : IMovement
{
    private readonly Transform target;
    private readonly float stopDistance;

    public TargetMovement(Transform target, float stopDistance = 1f)
    {
        this.target = target;
        this.stopDistance = stopDistance;
    }

    public Vector2 Move(Rigidbody2D rb, float moveSpeed)
    {
        if (target == null || IsInRange(rb.position, target.position))
        {
            return Vector2.zero;
        }

        ICommand command = new MoveTowardsCommand(rb, target.position, moveSpeed);
        command.Execute();

        return ((Vector2)target.position - rb.position).normalized;
    }

    private bool IsInRange(Vector2 currentPosition, Vector2 targetPosition)
    {
        return Vector2.Distance(currentPosition, targetPosition) <= stopDistance;
    }
}
