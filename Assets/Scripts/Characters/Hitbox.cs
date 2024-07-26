using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(EventTrigger))]
public class Hitbox : MonoBehaviour
{
    [SerializeField] private Character character;
    public Character Character => character;
    [SerializeField] private Collider2D _collider;
    
    private void Awake()
    {
        if (character == null)
        {
            character = GetComponentInParent<Character>();
            Debug.LogWarning($"Performance --> {character} {transform.parent.name} Hitbox Character is null in the Ispector!");
        }

        if (_collider == null)
        {
            Debug.LogWarning($"Performance --> {character} {transform.parent.name} Hitbox Collider2D is null in the Ispector!");
            _collider = GetComponent<BoxCollider2D>();
            Debug.Assert(_collider == null, $"Critical --> {transform.parent.name} Assign a Collider2D to {character} Hitbox!");
        }
    }
}
