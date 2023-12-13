using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputInterface : MonoBehaviour, DefaultControls.IPlayerActions
{
    public event Action Jumped = delegate { };
    public event Action Dodged = delegate { };
    public event Action MoveVectorChanged = delegate { };
    public event Action AttackBuffered = delegate { };

    public bool IsMovementVectorNonZero => _movementVector.magnitude > 0;
    public Vector2 MovementVector => _movementVector;
    public bool IsAttackBuffered => _attackBuffer.IsAttackBuffered;

    private Vector2 _movementVector;
    private DefaultControls _controls;
    private AttackBuffer _attackBuffer;

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
    public void OnLook(InputAction.CallbackContext context)
    {

    }
    public void OnZoom(InputAction.CallbackContext context)
    {

    }
    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _attackBuffer.BufferAttack();
            AttackBuffered();
        }
    }

    public void ClearAttackBuffer()
    {
        _attackBuffer.Clear();
    }
}