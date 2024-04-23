using Core;
using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Abstract
{
    public class Hero : MonoBehaviour
    {
        public event Action HorizontalDirectionChanged = delegate { };
        public event Action StartedJumping = delegate { };
        public event Action StoppedJumping = delegate { };
        public event Action StartedJumpAnimation = delegate { };
        public event Action FinishedBackflipAnimation = delegate { };
        public event Action ActionFrameEntered = delegate { };

        public int HorizontalDirection => _horizontalDirection;
        public float BaseRunSpeed => _baseRunSpeed;
        public float LeapMaxSpeed => _baseRunSpeed + _leapAdditionalSpeed;
        public AnimationCurve RunAccelerationCurve => _runAccelerationCurve;
        public int SpriteDirection => _animator.FlipX ? -1 : 1;
        public float CurrentFrameProgress => _animator.CurrentFrameProgress;
        public bool IsAnimatingIdle => _animator.IsAnimating(_idle);
        public bool IsAnimatingBackflip => _animator.IsAnimating(_backflip);
        public bool IsAnimatingJump => _animator.IsAnimating(_jump);
        public bool IsCurrentFrameFirstFrame => _animator.IsCurrentFrameFirstFrame;
        public bool IsTransitioningOnCurrentFrame => _animator.IsTransitioningOnCurrentFrame();
        public bool IsNextFrameActionFrame => _animator.IsNextFrameActionFrame;
        public bool IsPreviousFrameActionFrame => _animator.IsPreviousFrameActionFrame;
        public bool IsCurrentFrameActionFrame => _animator.IsCurrentFrameActionFrame;
        public float MinJumpTime => _minJumpTime;
        public float MaxJumpTime => _maxJumpTime;
        public bool IsJumping => _isJumping;
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
        private SpriteAnimation _landingAnimation;

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
        [SerializeField]
        private DirectionalAirControl _jumpAirControl;
        [SerializeField]
        private DirectionalAirControl _freeFallAirControl;

        [Header("Inputs")]
        [SerializeField, Range(-1, 1)]
        private int _horizontalDirection = 0;
        [SerializeField]
        private bool _isJumping = false;

        private void Awake()
        {
            if (_contacts.IsOnGround)
            {
                SetState(new HeroOnGroundState(this));
            }
            else
            {
                SetState(new HeroFreeFallState(this));
            }
        }
        private void OnEnable()
        {
            _animator.StartedAnimation += OnAnimationStarted;
            _animator.FinishedAnimation += OnAnimationFinished;
            _animator.ActionFrameEntered += OnActionFrameEntered;
        }
        private void OnDisable()
        {
            _animator.StartedAnimation -= OnAnimationStarted;
            _animator.FinishedAnimation -= OnAnimationFinished;
            _animator.ActionFrameEntered -= OnActionFrameEntered;
        }

        private void OnAnimationStarted()
        {
            if (_animator.IsAnimating(_jump))
            {
                StartedJumpAnimation();
            }
        }
        private void OnAnimationFinished()
        {
            if (_animator.IsAnimating(_backflip))
            {
                FinishedBackflipAnimation();
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
        public void ApplyJumpAirControl()
        {
            _jumpAirControl.ApplyTo(_physicsBody, _horizontalDirection, SpriteDirection);
        }
        public void ApplyFreeFallAirControl()
        {
            _freeFallAirControl.ApplyTo(_physicsBody, _horizontalDirection, SpriteDirection);
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

        public void SetLandingAnimation()
        {
            _animator.SetAnimation(_landingAnimation);
        }
        public void SetBackflipAnimation()
        {
            _animator.SetAnimation(_backflip);
        }

        public void TransitionFlipX(bool flipX)
        {
            _animator.TransitionFlipX(flipX);
        }
    }
}