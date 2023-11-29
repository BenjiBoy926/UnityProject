using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputInterface : MonoBehaviour, DefaultControls.IPlayerActions
{
    public event Action Jumped = delegate { };
    public event Action Dodged = delegate { };
    public event Action MoveVectorChanged = delegate { };

    public bool IsMovementVectorNonZero => _movementVector.magnitude > 0;
    public Vector2 MovementVector => _movementVector;

    private Vector2 _movementVector;
    private DefaultControls _controls;

    private void Awake()
    {
        _controls = new DefaultControls();
        _controls.Player.SetCallbacks(this);
        _controls.Enable();
    }
    private void OnDestroy()
    {
        _controls.Disable();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }
        Jumped();
    }
    public void OnDodge(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }
        Dodged();
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        _movementVector = context.ReadValue<Vector2>();
        MoveVectorChanged();
    }
}