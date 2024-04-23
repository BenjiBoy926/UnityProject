using System;
using UnityEngine;

namespace Abstract
{
    public class HeroInputs : MonoBehaviour
    {
        public event Action HorizontalDirectionChanged = delegate { };
        public event Action StartedJumping = delegate { };
        public event Action StoppedJumping = delegate { };
        public event Action DashDirectionChanged = delegate { };

        public int HorizontalDirection => _horizontalDirection;
        public bool IsJumping => _isJumping;

        [SerializeField, Range(-1, 1)]
        private int _horizontalDirection = 0;
        [SerializeField]
        private bool _isJumping;
        [SerializeField]
        private Vector2 _dashDirection;

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
        public void SetDashTarget(Vector2 target)
        {
            SetDashDirection(target - (Vector2)transform.position);
        }
        public void SetDashDirection(Vector2 direction)
        {
            direction = direction.normalized;
            if (direction == _dashDirection)
            {
                return;
            }
            _dashDirection = direction;
            DashDirectionChanged();
        }
    }
}