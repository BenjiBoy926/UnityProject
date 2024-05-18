using Core;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Abstract
{
    public class Hero : MonoBehaviour
    {
        public event Action HorizontalDirectionChanged = delegate { };
        public event Action DashAnimationFinished = delegate { };

        public int HorizontalDirection => _inputs.HorizontalDirection;
        public float BaseRunSpeed => _baseRunSpeed;
        public float LeapMaxSpeed => _baseRunSpeed + _leapAdditionalSpeed;
        public AnimationCurve RunAccelerationCurve => _runAccelerationCurve;
        public int FacingDirection => FlipXToDirection(_animator.FlipX);
        public bool SpriteFlipX => _animator.FlipX;
        public float CurrentFrameProgress => _animator.CurrentFrameProgress;
        public float ProgressAfterFirstActionFrame => _animator.ProgressAfterFirstActionFrame;
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
        public bool IsTouchingGround => _contacts.IsTouching(CardinalDirection.Down);
        public bool IsAimingDash => _inputs.IsAimingDash;
        public Vector2 DashAim => _inputs.DashAim;
        public float DashAimX => _inputs.DashAimX;
        public float DashAimY => _inputs.DashAimY;
        public float MaxDashSpeed => _maxDashSpeed;

        [Header("Parts")]
        [SerializeField]
        private HeroStateMachine _stateMachine;
        [SerializeField]
        private Rigidbody2D _physicsBody;
        [SerializeField]
        private SpriteAnimator _animator;
        [SerializeField]
        private CardinalContacts _contacts;
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
        private SpriteAnimation _midairDashAimAnimation;
        [SerializeField]
        private SpriteAnimation _sideDashAnimation;
        [SerializeField]
        private SpriteAnimation _upDashAnimation;

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

        [Header("Dashing")]
        [SerializeField]
        private float _aimingDashGravityScale = 0.1f;
        [SerializeField]
        private float _maxDashSpeed = 40;
        [SerializeField]
        private AnimationCurve _dashSpeedCurve;
        private float _defaultGravityScale;
        private Quaternion _defaultSpriteRotation;

        private void Awake()
        {
            _defaultGravityScale = _physicsBody.gravityScale;
            _defaultSpriteRotation = _animator.transform.localRotation;
            SetState(new HeroFreeFallState(this));
        }
        private void OnEnable()
        {
            _inputs.HorizontalDirectionChanged += OnHorizontalDirectionChanged;
            _animator.FinishedAnimation += OnAnimationFinished;
        }
        private void OnDisable()
        {
            _inputs.HorizontalDirectionChanged -= OnHorizontalDirectionChanged;
            _animator.FinishedAnimation -= OnAnimationFinished;
        }
        private void OnHorizontalDirectionChanged()
        {
            HorizontalDirectionChanged();
        }
        private void OnAnimationFinished()
        {
            if (_animator.IsAnimating(_upDashAnimation) || _animator.IsAnimating(_sideDashAnimation))
            {
                DashAnimationFinished();
            }
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
        public void SetDashTarget(Vector2 target)
        {
            _inputs.SetDashTarget(target);
        }
        public void SetIsAimingDash(bool isAimingDash)
        {
            _inputs.SetIsAimingDash(isAimingDash);
        }

        public void SetHorizontalVelocity(float velocity)
        {
            _physicsBody.SetVelocity(velocity, Dimension.X);
        }
        public void SetVerticalVelocity(float velocity)
        {
            _physicsBody.SetVelocity(velocity, Dimension.Y);
        }
        public void SetVelocity(Vector2 velocity)
        {
            _physicsBody.velocity = velocity;
        }

        public float EvaluateJumpSpeedCurve(float t)
        {
            return _jumpSpeedCurve.Evaluate(t);
        }
        public float EvaluateDashSpeedCurve(float t)
        {
            return _dashSpeedCurve.Evaluate(t);
        }
        public void ApplyJumpAirControl()
        {
            _jumpAirControl.ApplyTo(_physicsBody, HorizontalDirection, FacingDirection);
        }
        public void ApplyFreeFallAirControl()
        {
            _freeFallAirControl.ApplyTo(_physicsBody, HorizontalDirection, FacingDirection);
        }
        public void SetAimingDashGravityScale()
        {
            _physicsBody.gravityScale = _aimingDashGravityScale;
        }
        public void ResetGravityScale()
        {
            _physicsBody.gravityScale = _defaultGravityScale;
        }
        public void SetSpriteUp(Vector2 up)
        {
            _animator.transform.up = up;
        }
        public void SetSpriteRight(Vector2 right)
        {
            _animator.transform.right = right;
        }
        public void ResetSpriteRotation()
        {
            _animator.transform.localRotation = _defaultSpriteRotation;
        }

        public void TransitionToIdleAnimation(float transitionDurationScale, bool flipX)
        {
            _animator.SetTransition(new SpriteAnimationTransition(_idle, transitionDurationScale, flipX));
        }
        public void TransitionToRunAnimation(float transitionDurationScale, bool flipX)
        {   
            _animator.SetTransition(new SpriteAnimationTransition(_run, transitionDurationScale, flipX));
        }
        public void TransitionToJumpAnimation(float transitionDurationScale, bool flipX)
        {
            _animator.SetTransition(new SpriteAnimationTransition(_jump, transitionDurationScale, flipX));
        }
        public void TransitionToFreeFallForwardAnimation(float transitionDurationScale, bool flipX)
        {
            _animator.SetTransition(new SpriteAnimationTransition(_freeFallForward, transitionDurationScale, flipX));
        }
        public void TransitionToFreeFallBackwardAnimation(float transitionDurationScale, bool flipX)
        {
            _animator.SetTransition(new SpriteAnimationTransition(_freeFallBack, transitionDurationScale, flipX));
        }
        public void TransitionToFreeFallStraightAnimation(float transitionDurationScale, bool flipX)
        {
            _animator.SetTransition(new SpriteAnimationTransition(_freeFallStraight, transitionDurationScale, flipX));
        }
        public void TransitionToSquatAnimation(float transitionDurationScale, bool flipX)
        {
            _animator.SetTransition(new SpriteAnimationTransition(_squatAnimation, transitionDurationScale, flipX));
        }
        public void TransitionToMidairDashAimAnimation(float transitionDurationScale, bool flipX)
        {
            _animator.SetTransition(new SpriteAnimationTransition(_midairDashAimAnimation, transitionDurationScale, flipX));
        }
        public void SetSideDashAnimation()
        {
            _animator.SetAnimation(_sideDashAnimation);
        }
        public void SetUpDashAnimation()
        {
            _animator.SetAnimation(_upDashAnimation);
        }

        public void SetSquatAnimation()
        {
            _animator.SetAnimation(_squatAnimation);
        }
        public void SetBackflipAnimation()
        {
            _animator.SetAnimation(_backflip);
        }
        public void FaceTowards(float direction)
        {
            _animator.SetFlipX(DirectionToFlipX(direction));
        }

        public int GetContactCount()
        {
            return _contacts.GetContactCount();
        }
        public IEnumerable<Vector2> GetContactNormals()
        {
            return _contacts.GetContactNormals();
        }

        public static bool DirectionToFlipX(float direction)
        {
            return direction < 0;
        }
        public static int FlipXToDirection(bool flip)
        {
            return flip ? -1 : 1;
        }
    }
}