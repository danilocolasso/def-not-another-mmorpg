using UnityEngine;

public interface IMovement
{
    public Vector2 Move(Rigidbody2D rb, float moveSpeed);
}
