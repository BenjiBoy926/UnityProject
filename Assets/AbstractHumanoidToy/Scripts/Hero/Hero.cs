using System;
using UnityEngine;

namespace AbstractHumanoidToy
{
    public class Hero : MonoBehaviour
    {
        public event Action CurrentDirectionChanged = delegate { };

        public int CurrentDirection => _currentDirection;

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
        [SerializeField]
        private AnimationCurve _accelerationCurve;
        [SerializeField, Range(-1, 1)]
        private int _currentDirection = 0;
        private FromToCurve _toRun;
        private FromToCurve _toLeap;

        private void Awake()
        {
            _stateMachine.SetState(new HeroOnGroundState(this));
        }
        public void SetCurrentDirection(int currentDirection)
        {
            if (_currentDirection == currentDirection)
            {
                return;
            }
            _currentDirection = currentDirection;
            CurrentDirectionChanged();
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