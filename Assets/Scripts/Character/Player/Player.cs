using UnityEngine;

[RequireComponent(typeof(InputHandler))]
public class Player : Character
{
    private InputHandler inputHandler;

    protected override void Awake()
    {
        base.Awake();
        inputHandler = GetComponent<InputHandler>();
        SetMovement(new InputMovement(inputHandler));
    }
}
