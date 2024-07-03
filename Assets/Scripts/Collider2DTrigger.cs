using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CircleCollider2D))]
public class Collider2DTrigger : MonoBehaviour
{
    private CircleCollider2D coll;
    public UnityEvent<Collider2D> OnTriggerEnter;
    public UnityEvent<Collider2D> OnTriggerExit;

    public void SetRadius(float radius)
    {
        coll.radius = radius;
    }

    private void Awake()
    {
        coll = GetComponent<CircleCollider2D>();
        coll.isTrigger = true;
    }

    /**
    * Select the DYNAMIC option in inspector
    */
    private void OnTriggerEnter2D(Collider2D other)
    {
        OnTriggerEnter.Invoke(other);
    }
    
    /**
    * Select the DYNAMIC option in inspector
    */
    private void OnTriggerExit2D(Collider2D other)
    {
        OnTriggerExit.Invoke(other);
    }

    public void Enable()
    {
        coll.enabled = true;
    }

    public void Disable()
    {
        coll.enabled = false;
    }
}
