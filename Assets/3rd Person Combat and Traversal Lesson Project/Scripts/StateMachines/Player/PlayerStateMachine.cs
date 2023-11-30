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
    private Transform _body;
    [SerializeField]
    private PlayerStats _stats;
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private Transform _cameraTransform;

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

    public void TurnTowardsDirection(Vector2 direction)
    {
        Vector3 direction3D = ConvertAnalogDirectionToWorldDirection(direction);
        Quaternion targetRotation = Quaternion.LookRotation(direction3D);
        Quaternion nextRotation = Quaternion.RotateTowards(_body.rotation, targetRotation, _stats.MaxDegreeTurnPerSecond * DeltaTime);
        _body.rotation = nextRotation;
    }
    private Vector3 ConvertAnalogDirectionToWorldDirection(Vector2 analog)
    {
        Vector3 world = new Vector3(analog.x, 0, analog.y);
        world = _cameraTransform.TransformDirection(world);
        world.y = 0;
        return world;
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