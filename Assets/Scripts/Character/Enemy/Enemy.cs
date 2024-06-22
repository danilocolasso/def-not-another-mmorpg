using UnityEngine;

public class Enemy : Character
{
    [SerializeField] private Transform target;
    [SerializeField][Range(0, 10)] private float attackRange = 5f;

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (target != null) {
            SetMovement(new TargetMovement(target, attackRange));
        }
    }
}
