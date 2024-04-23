using Core;
using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Abstract
{
    public class Hero : MonoBehaviour
    {
        public event Action HorizontalDirectionChanged = delegate { };
        public event Action IsJumpingChanged = delegate { };
        public event Action StartedJumpAnimation = delegate { };
        public event Action ActionFrameEntered = delegate { };
        public event Action DashDirectionChanged = delegate { };
        public event Action IsAimingDashChanged = delegate { };

        public int HorizontalDirection => _inputs.HorizontalDirection;
        public float BaseRunSpeed => _baseRunSpeed;
        public float LeapMaxSpeed => _baseRunSpeed + _leapAdditionalSpeed;
        public AnimationCurve RunAccelerationCurve => _runAccelerationCurve;
        public int SpriteDirection => _animator.FlipX ? -1 : 1;
        public float CurrentFrameProgress => _animator.CurrentFrameProgress;
        public bool IsAnimatingIdle => _animator.IsAnimating(_idle);
        public bool IsAnimatingJump => _animator.IsAnimating(_jump);
        public bool IsCurrentFrameFirstFrame => _animator.IsCurrentFrameFirstFrame;
        public bool IsTransitioningOnCurrentFrame => _animator.IsTransitioningOnCurrentFrame();
        public bool IsNextFrameActionFrame => _animator.IsNextFrameActionFrame;
        public bool IsPreviousFrameActionFrame => _animator.IsPreviousFrameActionFrame;
        public bool IsCurrentFrameActionFrame => _animator.IsCurrentFrameActionFrame;
        public float MaxJumpSpeed => _maxJumpSpeed;
        public float MinJumpTime => _minJumpTime;
        public float MaxJumpTime => _maxJumpTime;
        public bool IsJumping => _inputs.IsJumping;
        public bool IsOnGround => _contacts.IsOnGround;

        [Header("Parts")]
        [SerializeField]
        private HeroStateMachine _stateMachine;
        [SerializeField]
        private Rigidbody2D _physicsBody;
        [SerializeField]
        private SpriteAnimator _animator;
        [SerializeField]
        private HeroContacts _contacts;
        [SerializeField]
        private HeroInputs _inputs;

        [Header("Animations")]
        [SerializeField]
        private SpriteAnimation _idle;
        [SerializeField]
        private SpriteAnimation _run;
        [SerializeField]
        private SpriteAnimation _jump;
        [SerializeField]
        private SpriteAnimation _backflip;
        [SerializeField]
        private SpriteAnimation _freeFallForward;
        [SerializeField]
        private SpriteAnimation _freeFallBack;
        [SerializeField]
        private SpriteAnimation _freeFallStraight;
        [SerializeField]
        private SpriteAnimation _squatAnimation;
        [SerializeField]
        private SpriteAnimation _sideDashPrepAnimation;

        [Header("Running")]
        [SerializeField]
        private float _baseRunSpeed = 1;
        [SerializeField]
        private float _leapAdditionalSpeed = 1;
        [SerializeField, FormerlySerializedAs("_accelerationCurve")]
        private AnimationCurve _runAccelerationCurve;

        [Header("Jumping")]
        [SerializeField]
        private float _maxJumpSpeed = 5;
        [SerializeField]
        private AnimationCurve _jumpSpeedCurve;
        [SerializeField]
        private float _minJumpTime = 0.1f;
        [SerializeField]
        private float _maxJumpTime = 1;
        [SerializeField]
        private DirectionalAirControl _jumpAirControl;
        [SerializeField]
        private DirectionalAirControl _freeFallAirControl;

        private void Awake()
        {
            SetState(new HeroFreeFallState(this));
        }
        private void OnEnable()
        {
            _animator.StartedAnimation += OnAnimationStarted;
            _animator.ActionFrameEntered += OnActionFrameEntered;
            _inputs.HorizontalDirectionChanged += OnHorizontalDirectionChanged;
            _inputs.IsJumpingChanged += OnIsJumpingChanged;
            _inputs.DashDirectionChanged += OnDashDirectionChanged;
            _inputs.IsAimingDashChanged += OnIsAimingDashChanged;
        }


        private void OnDisable()
        {
            _animator.StartedAnimation -= OnAnimationStarted;
            _animator.ActionFrameEntered -= OnActionFrameEntered;
            _inputs.HorizontalDirectionChanged -= OnHorizontalDirectionChanged;
            _inputs.IsAimingDashChanged -= OnIsAimingDashChanged;
            _inputs.DashDirectionChanged -= OnDashDirectionChanged;
        }
        private void OnAnimationStarted()
        {
            if (_animator.IsAnimating(_jump))
            {
                StartedJumpAnimation();
            }
        }
        private void OnActionFrameEntered()
        {
            ActionFrameEntered();
        }
        private void OnHorizontalDirectionChanged()
        {
            HorizontalDirectionChanged();
        }
        private void OnIsJumpingChanged()
        {
            IsJumpingChanged();
        }
        private void OnDashDirectionChanged()
        {
            DashDirectionChanged();
        }
        private void OnIsAimingDashChanged()
        {
            IsAimingDashChanged();
        }

        public void SetState(HeroState state)
        {
            _stateMachine.SetState(state);
        }
        public void SetHorizontalDirection(int horizontalDirection)
        {
            _inputs.SetHorizontalDirection(horizontalDirection);
        }
        public void SetIsJumping(bool isJumping)
        {
            _inputs.SetIsJumping(isJumping);
        }
        public void SetHorizontalVelocity(float velocity)
        {
            _physicsBody.SetVelocity(velocity, Dimension.X);
        }
        public void SetVerticalVelocity(float velocity)
        {
            _physicsBody.SetVelocity(velocity, Dimension.Y);
        }
        public float EvaluateJumpSpeedCurve(float t)
        {
            return _jumpSpeedCurve.Evaluate(t);
        }
        public void ApplyJumpAirControl()
        {
            _jumpAirControl.ApplyTo(_physicsBody, HorizontalDirection, SpriteDirection);
        }
        public void ApplyFreeFallAirControl()
        {
            _freeFallAirControl.ApplyTo(_physicsBody, HorizontalDirection, SpriteDirection);
        }

        public void TransitionToIdleAnimation(float transitionDurationScale)
        {
            _animator.TransitionTo(_idle, transitionDurationScale);
        }
        public void TransitionToRunAnimation(float transitionDurationScale)
        {   
            _animator.TransitionTo(_run, transitionDurationScale);
        }
        public void TransitionToJumpAnimation(float transitionDurationScale)
        {
            _animator.TransitionTo(_jump, transitionDurationScale);
        }
        public void TransitionToFreeFallForwardAnimation(float transitionDurationScale)
        {
            _animator.TransitionTo(_freeFallForward, transitionDurationScale);
        }
        public void TransitionToFreeFallBackwardAnimation(float transitionDurationScale)
        {
            _animator.TransitionTo(_freeFallBack, transitionDurationScale);
        }
        public void TransitionToFreeFallStraightAnimation(float transitionDurationScale)
        {
            _animator.TransitionTo(_freeFallStraight, transitionDurationScale);
        }

        public void SetSquatAnimation()
        {
            _animator.SetAnimation(_squatAnimation);
        }
        public void SetBackflipAnimation()
        {
            _animator.SetAnimation(_backflip);
        }
        public void SetSideDashPrepAnimation()
        {
            _animator.SetAnimation(_sideDashPrepAnimation);
        }

        public void TransitionFlipX(bool flipX)
        {
            _animator.TransitionFlipX(flipX);
        }

        public void SetDashTarget(Vector2 target)
        {
            _inputs.SetDashTarget(target);
        }
    }
}