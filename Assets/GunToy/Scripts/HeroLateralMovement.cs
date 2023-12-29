using System.Collections;
using UnityEngine;

namespace GunToy
{
    public class HeroLateralMovement : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody2D _rigidbody;
        [SerializeField]
        private float _exactMoveSpeed = 5;
        [SerializeField]
        private float _highSpeedAcceleration = 5;
        [SerializeField]
        private float _currentMoveDirection = 0;

        public void SetCurrentMoveDirection(float currentMoveDirection)
        {
            _currentMoveDirection = currentMoveDirection;
        }

        private void Update()
        {
            Vector3 currentVelocity = _rigidbody.velocity;
            if (Mathf.Abs(currentVelocity.x) > _exactMoveSpeed)
            {
                ApplyForceMovement();
            }
            else
            {
                ApplyExactMovement(currentVelocity);
            }
        }
        private void ApplyExactMovement(Vector3 currentVelocity)
        {
            if (_currentMoveDirection == 0)
            {
                return;
            }
            currentVelocity.x = _currentMoveDirection * _exactMoveSpeed;
            _rigidbody.velocity = currentVelocity;
        }
        private void ApplyForceMovement()
        {
            Vector2 force = _currentMoveDirection * _highSpeedAcceleration * Vector2.right;
            _rigidbody.AddForce(force);
        }
    }
}