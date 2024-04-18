using Core;
using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace AbstractHumanoidToy
{
    public class Hero : MonoBehaviour
    {
        public event Action HorizontalDirectionChanged = delegate { };

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

        [SerializeField]
        private HeroStateMachine _stateMachine;
        [SerializeField]
        private Rigidbody2D _physicsBody;
        [SerializeField]
        private SpriteAnimator _animator;
        [SerializeField]
        private SpriteAnimation _idle;
        [SerializeField]
        private SpriteAnimation _run;
        [SerializeField]
        private float _baseRunSpeed = 1;
        [SerializeField]
        private float _leapAdditionalSpeed = 1;
        [SerializeField, FormerlySerializedAs("_accelerationCurve")]
        private AnimationCurve _runAccelerationCurve;
        [SerializeField, Range(-1, 1)]
        private int _horizontalDirection = 0;

        private void Awake()
        {
            SetState(new HeroOnGroundState(this));
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
        public void SetHorizontalVelocity(float velocity)
        {
            _physicsBody.SetVelocity(velocity, Dimension.X);
        }
        public void TransitionToIdle()
        {
            _animator.TransitionTo(_idle);
        }
        public void TransitionToRun()
        {
            _animator.TransitionTo(_run);
        }
        public void TransitionFlipX(bool flipX)
        {
            _animator.TransitionFlipX(flipX);
        }
    }
}