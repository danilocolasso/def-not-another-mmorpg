using UnityEngine;

public class DetectionZone : MonoBehaviour
{
    private Character character;

    private void Awake()
    {
        character = GetComponentInParent<Character>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Character otherCharacter = other.GetComponent<Character>();

        if (otherCharacter != null && otherCharacter != character)
        {
            character.SetTarget(otherCharacter);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Character otherCharacter = other.GetComponent<Character>();

        if (otherCharacter != null && otherCharacter == character.Target)
        {
            character.SetTarget(null);
        }
    }
}
