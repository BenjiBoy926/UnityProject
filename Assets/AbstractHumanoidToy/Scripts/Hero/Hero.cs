using Core;
using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace AbstractHumanoidToy
{
    public class Hero : MonoBehaviour
    {
        public event Action HorizontalDirectionChanged = delegate { };
        public event Action StartedJumping = delegate { };
        public event Action StoppedJumping = delegate { };
        public event Action StartedJumpAnimation = delegate { };
        public event Action ActionFrameEntered = delegate { };

        public int HorizontalDirection => _horizontalDirection;
        public float BaseRunSpeed => _baseRunSpeed;
        public float LeapMaxSpeed => _baseRunSpeed + _leapAdditionalSpeed;
        public AnimationCurve RunAccelerationCurve => _runAccelerationCurve;
        public float SpriteDirection => _animator.FlipX ? -1 : 1;
        public float CurrentFrameProgress => _animator.CurrentFrameProgress;
        public bool IsAnimatingIdle => _animator.IsAnimating(_idle);
        public bool IsCurrentFrameFirstFrame => _animator.IsCurrentFrameFirstFrame;
        public bool IsTransitioningOnCurrentFrame => _animator.IsTransitioningOnCurrentFrame();
        public bool IsNextFrameActionFrame => _animator.IsNextFrameActionFrame;
        public bool IsPreviousFrameActionFrame => _animator.IsPreviousFrameActionFrame;
        public bool IsCurrentFrameActionFrame => _animator.IsCurrentFrameActionFrame;
        public float MinJumpTime => _minJumpTime;
        public float MaxJumpTime => _maxJumpTime;
        public bool IsJumping => _isJumping;

        [Header("Parts")]
        [SerializeField]
        private HeroStateMachine _stateMachine;
        [SerializeField]
        private Rigidbody2D _physicsBody;
        [SerializeField]
        private SpriteAnimator _animator;

        [Header("Animations")]
        [SerializeField]
        private SpriteAnimation _idle;
        [SerializeField]
        private SpriteAnimation _run;
        [SerializeField]
        private SpriteAnimation _jump;

        [Header("Running")]
        [SerializeField]
        private float _baseRunSpeed = 1;
        [SerializeField]
        private float _leapAdditionalSpeed = 1;
        [SerializeField, FormerlySerializedAs("_accelerationCurve")]
        private AnimationCurve _runAccelerationCurve;

        [Header("Jumping")]
        [SerializeField]
        private float _jumpSpeed = 5;
        [SerializeField]
        private float _minJumpTime = 0.1f;
        [SerializeField]
        private float _maxJumpTime = 1;

        [Header("Inputs")]
        [SerializeField, Range(-1, 1)]
        private int _horizontalDirection = 0;
        [SerializeField]
        private bool _isJumping = false;

        private void Awake()
        {
            SetState(new HeroOnGroundState(this));
        }
        private void OnEnable()
        {
            _animator.StartedAnimation += OnAnimationStarted;
            _animator.ActionFrameEntered += OnActionFrameEntered;
        }
        private void OnDisable()
        {
            _animator.StartedAnimation -= OnAnimationStarted;
            _animator.ActionFrameEntered -= OnActionFrameEntered;
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

        public void SetState(HeroState state)
        {
            _stateMachine.SetState(state);
        }
        public void SetHorizontalDirection(int horizontalDirection)
        {
            if (_horizontalDirection == horizontalDirection)
            {
                return;
            }
            _horizontalDirection = horizontalDirection;
            HorizontalDirectionChanged();
        }
        public void SetIsJumping(bool isJumping)
        {
            if (isJumping == _isJumping)
            {
                return;
            }
            _isJumping = isJumping;
            if (_isJumping)
            {
                StartedJumping();
            }
            else
            {
                StoppedJumping();
            }
        }
        public void SetHorizontalVelocity(float velocity)
        {
            _physicsBody.SetVelocity(velocity, Dimension.X);
        }
        public void SetJumpingVelocity()
        {
            _physicsBody.SetVelocity(_jumpSpeed, Dimension.Y);
        }
        public void TransitionToIdleAnimation()
        {
            _animator.TransitionTo(_idle);
        }
        public void TransitionToRunAnimation()
        {
            _animator.TransitionTo(_run);
        }
        public void TransitionToJumpAnimation()
        {
            _animator.TransitionTo(_jump);
        }
        public void TransitionFlipX(bool flipX)
        {
            _animator.TransitionFlipX(flipX);
        }
    }
}