using System;
using UnityEngine;

namespace Abstract
{
    public class HeroInputs : MonoBehaviour
    {
        public event Action HorizontalDirectionChanged = delegate { };
        public event Action StartedJumping = delegate { };
        public event Action StoppedJumping = delegate { };

        public int HorizontalDirection => _horizontalDirection;
        public bool IsJumping => _isJumping;

        [SerializeField, Range(-1, 1)]
        private int _horizontalDirection = 0;
        [SerializeField]
        private bool _isJumping;

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
    }
}