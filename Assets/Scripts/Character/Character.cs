using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Character : MonoBehaviour
{
    [SerializeField] protected float moveSpeed = 5f;

    protected Rigidbody2D rb;
    protected MovementInterface movement;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void FixedUpdate()
    {
        movement?.Move(rb, moveSpeed);
    }

    public void SetMovement(MovementInterface newMovement)
    {
        movement = newMovement;
    }
}
