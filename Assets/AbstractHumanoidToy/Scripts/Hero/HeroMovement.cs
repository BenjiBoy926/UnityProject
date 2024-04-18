using Core;
using DG.Tweening;
using UnityEngine;

namespace AbstractHumanoidToy
{
    public class HeroMovement : MonoBehaviour
    {
        private float LeapMaxSpeed => _baseRunSpeed + _leapAdditionalSpeed;
        private int ApparentDirection
        {
            get
            {
                if (_animator.IsAnimating(_idle))
                {
                    return 0;
                }
                return FlipXToDirection(_animator.FlipX);
            }
        }

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
        [SerializeField]
        private AnimationCurve _accelerationCurve;
        [SerializeField]
        private AnimationCurve _decelerationCurve;
        [SerializeField, Range(-1, 1)]
        private int _currentDirection = 0;

        private void Reset()
        {
            _physicsBody = GetComponentInChildren<Rigidbody2D>();
            _animator = GetComponentInChildren<SpriteAnimator>();
        }
        private void OnValidate()
        {
            if (_physicsBody == null || _animator == null)
            {
                return;
            }
            ReflectCurrentDirection();
        }
        private void OnEnable()
        {
            ReflectCurrentDirection();
        }
        private void Update()
        {
            float speed = CalculateHorizontalSpeed();
            _physicsBody.SetVelocity(speed, Dimension.X);
        }
        private float CalculateHorizontalSpeed()
        {
            if (ApparentDirection == 0)
            {
                return 0;
            }
            if (_animator.IsCurrentFrameFirstFrame)
            {
                return ApparentDirection * _baseRunSpeed * _accelerationCurve.Evaluate(_animator.CurrentFrameProgress);
            }
            if (_animator.IsSmoothStoppingOnCurrentFrame())
            {
                return ApparentDirection * _baseRunSpeed * _decelerationCurve.Evaluate(_animator.CurrentFrameProgress);
            }
            if (_animator.IsNextFrameActionFrame)
            {
                return ApparentDirection * (_baseRunSpeed + (_leapAdditionalSpeed * _accelerationCurve.Evaluate(_animator.CurrentFrameProgress)));
            }
            if (_animator.IsPreviousFrameActionFrame)
            {
                return ApparentDirection * (_baseRunSpeed + (_leapAdditionalSpeed * _decelerationCurve.Evaluate(_animator.CurrentFrameProgress)));
            }
            if (_animator.IsCurrentFrameActionFrame)
            {
                return ApparentDirection * (_baseRunSpeed + _leapAdditionalSpeed);
            }
            return ApparentDirection * _baseRunSpeed;
        }

        public void SetCurrentDirection(int direction)
        {
            if (_currentDirection == direction)
            {
                return;
            }
            _currentDirection = direction;
            ReflectCurrentDirection();
        }
        private void ReflectCurrentDirection()
        {
            TransitionAnimation();
            TransitionFlipX();
        }
        private void TransitionAnimation()
        {
            if (_currentDirection == 0)
            {
                _animator.TransitionTo(_idle);
            }
            else
            {
                _animator.TransitionTo(_run);
            }
        }
        private void TransitionFlipX()
        {
            if (_currentDirection != 0)
            {
                _animator.TransitionFlipX(DirectionToFlipX(_currentDirection));
            }
        }
        private static bool DirectionToFlipX(int direction)
        {
            return direction == -1;
        }
        private static int FlipXToDirection(bool flipX)
        {
            return flipX ? -1 : 1;
        }
    }
}