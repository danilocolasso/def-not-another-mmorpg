using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(EventTrigger))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class Hitbox : MonoBehaviour
{
    [SerializeField] private Character character;
    public Character Character => character;
    
    private void Awake()
    {
        if (character == null)
        {
            character = GetComponentInParent<Character>();
            Debug.LogWarning($"Performance --> {character} Hitbox is null in the Ispector!");
        }
    }
}
