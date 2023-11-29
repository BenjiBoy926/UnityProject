using System;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    private const string FreeLookSpeedAnimatorParameterName = "FreeLookSpeed";

    public event Action Jumped = delegate { };
    public event Action Dodged = delegate { };
    public event Action MovementVectorChanged = delegate { };

    public bool IsMovementVectorNonZero => _input.IsMovementVectorNonZero;
    public Vector2 MovementVector => _input.MovementVector;

    [SerializeField]
    private PlayerInputInterface _input;
    [SerializeField]
    private Rigidbody _rigidbody;
    [SerializeField]
    private PlayerStats _stats;
    [SerializeField]
    private Animator _animator;

    protected override void Awake()
    {
        base.Awake();
        _input.Jumped += OnJump;
        _input.Dodged += OnDodge;
        _input.MoveVectorChanged += OnMovementVectorChanged;
        SetState(new PlayerTestState(this, "Move"));
    }
    private void OnDestroy()
    {
        _input.Jumped -= OnJump;
        _input.Dodged -= OnDodge;
        _input.MoveVectorChanged -= OnMovementVectorChanged;
    }

    private void OnJump()
    {
        Jumped();
    }
    private void OnDodge()
    {
        Dodged();
    }
    private void OnMovementVectorChanged()
    {
        MovementVectorChanged();
    }

    public void Move(Vector2 direction)
    {
        Vector3 translation = new Vector3(direction.x, 0, direction.y) * _stats.MoveSpeed * DeltaTime;
        Vector3 newPosition = _rigidbody.position + translation;
        Quaternion newRotation = Quaternion.LookRotation(translation);
        _rigidbody.Move(newPosition, newRotation);
    }
    public void BlendTowardsIdleAnimation()
    {
        _animator.SetFloat(FreeLookSpeedAnimatorParameterName, 0, 0.1f, DeltaTime);
    }
    public void BlendTowardsWalkingAnimation()
    {
        _animator.SetFloat(FreeLookSpeedAnimatorParameterName, 1, 0.1f, DeltaTime);
    }
}