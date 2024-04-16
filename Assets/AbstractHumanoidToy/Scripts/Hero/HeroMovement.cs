using UnityEngine;

namespace AbstractHumanoidToy
{
    public class HeroMovement : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody2D _physicsBody;
        [SerializeField]
        private SpriteBody _spriteBody;
        [SerializeField]
        private SpriteAnimator _animator;
        [SerializeField]
        private SpriteAnimation _idle;
        [SerializeField] 
        private SpriteAnimation _run;
        [SerializeField, Range(-1, 1)]
        private int _currentDirection = 0;

        private void Reset()
        {
            _physicsBody = GetComponentInChildren<Rigidbody2D>();
            _spriteBody = GetComponentInChildren<SpriteBody>();
            _animator = GetComponentInChildren<SpriteAnimator>();
        }
        private void OnValidate()
        {
            if (_physicsBody == null || _spriteBody == null || _animator == null)
            {
                return;
            }
            ReflectCurrentDirection();
        }
        private void OnEnable()
        {
            _animator.ActionFrameEntered += OnActionFrameEntered;
            ReflectCurrentDirection();
        }
        private void OnDisable()
        {
            _animator.ActionFrameEntered -= OnActionFrameEntered;
        }

        private void OnActionFrameEntered()
        {
            Debug.Log("Action frame entered");
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
            if (_currentDirection == -1)
            {
                _animator.TransitionFlipX(true);
            }
            if (_currentDirection == 1)
            {
                _animator.TransitionFlipX(false);
            }
        }
    }
}