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
        [SerializeField, Range(-1, 1)]
        private int _currentDirection = 0;
        private FromToCurve _toRun;
        private FromToCurve _toLeap;

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
        private void Awake()
        {
            _toRun = new FromToCurve(0, _baseRunSpeed, _accelerationCurve);
            _toLeap = new FromToCurve(_baseRunSpeed, LeapMaxSpeed, _accelerationCurve);
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
                return ApparentDirection * _toRun.Evaluate(_animator.CurrentFrameProgress);
            }
            if (_animator.IsTransitioningOnCurrentFrame())
            {
                return ApparentDirection * _toRun.Evaluate(1 - _animator.CurrentFrameProgress);
            }
            if (_animator.IsNextFrameActionFrame)
            {
                return ApparentDirection * _toLeap.Evaluate(_animator.CurrentFrameProgress);
            }
            if (_animator.IsPreviousFrameActionFrame)
            {
                return ApparentDirection * _toLeap.Evaluate(1 - _animator.CurrentFrameProgress);
            }
            if (_animator.IsCurrentFrameActionFrame)
            {
                return ApparentDirection * LeapMaxSpeed;
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