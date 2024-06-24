using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    private PlayerControls playerControls;

    public Vector2 MovementInput { get; private set; }

    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        playerControls.Player.Enable();
        playerControls.Player.Move.performed += OnMove;
        playerControls.Player.Move.canceled += OnMoveCanceled;
    }

    private void OnDisable()
    {
        playerControls.Player.Disable();
        playerControls.Player.Move.performed -= OnMove;
        playerControls.Player.Move.canceled -= OnMoveCanceled;
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        MovementInput = context.ReadValue<Vector2>();
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        MovementInput = Vector2.zero;
    }
}
