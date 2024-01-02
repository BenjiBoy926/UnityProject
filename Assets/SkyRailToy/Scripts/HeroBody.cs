using System.Collections;
using UnityEngine;
using NaughtyAttributes;

namespace SkyRailToy
{
    public class HeroBody : MonoBehaviour
    {
        public bool IsJumping => _isJumping;
        private float TimeSinceJumpStart => Time.time - _timeOfJumpStart;

        [SerializeField]
        private HeroStats _stats;
        [SerializeField]
        private Rigidbody2D _rigidbody;
        [SerializeField]
        private int _lateralDirection;
        [SerializeField]
        private bool _isJumping;
        private float _timeOfJumpStart;

        public void SetLateralDirection(int lateralDirection)
        {
            _lateralDirection = lateralDirection;
        }
        [Button("Start Jumping")]
        public void StartJumping()
        {
            _isJumping = true;
            _timeOfJumpStart = Time.time;
        }
        public void StopJumping()
        {
            _isJumping = false;
        }

        private void Update()
        {
            UpdateLateralMovement();
            UpdateJump();
        }
        private void UpdateLateralMovement()
        {
            SetVelocityAlongAxis(_lateralDirection * _stats.MoveSpeed, 0);
        }
        private void UpdateJump()
        {
            if (!_isJumping)
            {
                return;
            }
            SetVelocityAlongAxis(_stats.JumpSpeed, 1);
            if (TimeSinceJumpStart > _stats.MaxJumpTime)
            {
                StopJumping();
            }
        }
        private void SetVelocityAlongAxis(float velocity, int axis)
        {
            Vector2 currentVelocity = _rigidbody.velocity;
            currentVelocity[axis] = velocity;
            _rigidbody.velocity = currentVelocity;
        }
    }
}