using System;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerStateMachine : StateMachine
{
    private readonly int FreeLookSpeedPropertyHash = Animator.StringToHash("FreeLookSpeed");
    private readonly int FreeLookStateHash = Animator.StringToHash("FreeLookBlendTree");

    public event Action Jumped = delegate { };
    public event Action Dodged = delegate { };
    public event Action MovementVectorChanged = delegate { };
    public event Action AttackBuffered = delegate { };

    public float TimeToTurnAroundWhileWalking => _stats.TimeToTurnAroundWhileWalking;
    public bool IsMovementVectorNonZero => _input.IsMovementVectorNonZero;
    /// <summary>
    /// The input movement vector transformed by the direction that the player's camera is facing
    /// </summary>
    public Vector3 WorldMovementVector => ConvertAnalogDirectionToWorldDirection(InputMovementVector);
    /// <summary>
    /// The movement vector from the game's input device, such as arrow keys and WASD inputs from a keyboard
    /// or left analog stick position for a game pad
    /// </summary>
    public Vector2 InputMovementVector => _input.MovementVector;
    public bool IsAttackBuffered => _input.IsAttackBuffered;
    public int ComboLength => _stats.ComboLength;
    public Vector3 Heading => _bodyTransform.forward;

    [SerializeField]
    private PlayerInputInterface _input;
    [SerializeField]
    [FormerlySerializedAs("_body")]
    private Transform _bodyTransform;
    [SerializeField]
    private Rigidbody _body;
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
        _input.AttackBuffered += OnAttackBuffered;
        SetState(new PlayerFreeLookState(this));
    }
    private void OnDestroy()
    {
        _input.Jumped -= OnJump;
        _input.Dodged -= OnDodge;
        _input.MoveVectorChanged -= OnMovementVectorChanged;
        _input.AttackBuffered -= OnAttackBuffered;
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
    private void OnAttackBuffered()
    {
        AttackBuffered();
    }

    public void TurnTowardsDirection(Vector3 direction, float timeToTurnAround)
    {
        float degreeTurnPerSecond = 180 / timeToTurnAround;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        Quaternion nextRotation = Quaternion.RotateTowards(_bodyTransform.rotation, targetRotation, degreeTurnPerSecond * DeltaTime);
        _bodyTransform.rotation = nextRotation;
    }
    public void Leap(Vector3 velocity)
    {
        _body.velocity = velocity;
    }
    public Vector3 ConvertAnalogDirectionToWorldDirection(Vector2 analog)
    {
        Vector3 world = new Vector3(analog.x, 0, analog.y);
        world = _cameraTransform.TransformDirection(world);
        world.y = 0;
        return world.normalized;
    }

    public void BlendTowardsIdleAnimation()
    {
        _animator.SetFloat(FreeLookSpeedPropertyHash, 0, _stats.IdleToWalkTransitionTime, DeltaTime);
    }
    public void BlendTowardsWalkingAnimation()
    {
        _animator.SetFloat(FreeLookSpeedPropertyHash, 1, _stats.IdleToWalkTransitionTime, DeltaTime);
    }
    public void BlendToFreeLookAnimationState(float time)
    {
        _animator.CrossFadeInFixedTime(FreeLookStateHash, time);
    }
    public void BlendToAttackAnimation(int attackIndex)
    {
        AttackInfo attack = _stats.GetAttack(attackIndex);
        _animator.CrossFadeInFixedTime(attack.AnimationName, attack.TransitionDuration);
    }

    public void ClearAttackBuffer()
    {
        _input.ClearAttackBuffer();
    }
    public AttackInfo GetAttack(int index)
    {
        return _stats.GetAttack(index);
    }

    public void SetRootMotionActive(bool rootMotionActive)
    {
        _animator.applyRootMotion = rootMotionActive;
    }
}