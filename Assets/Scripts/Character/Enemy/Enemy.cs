using UnityEngine;

public class Enemy : Character
{
    [SerializeField] private Transform target;

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (target != null) {
            SetMovement(new TargetMovement(target));
        }
    }
}
