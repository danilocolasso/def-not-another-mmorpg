using UnityEngine;

public class MovementManager : MonoBehaviour
{
    private Character character;
    private IMovement movement;

    private void Awake()
    {
        character = GetComponent<Character>();
    }

    public void FixedUpdate()
    {
        movement?.Move(character.Rb, character.Data.Status.MoveSpeed);
    }

    public void SetMovement(IMovement newMovement)
    {
        movement = newMovement;
    }
}
