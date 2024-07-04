using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(EventTrigger))]
[RequireComponent(typeof(CircleCollider2D))]
public class Hitbox : MonoBehaviour
{
    public Character Character { get; private set; }

    private void Awake()
    {
        Character = GetComponentInParent<Character>();
    }
}
