using System.Collections;
using UnityEngine;

namespace GunToy
{
    public class HeroLegs : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody2D _rigidbody;
        [SerializeField]
        private float _moveSpeed = 5;
        [SerializeField]
        private float _currentMoveDirection = 0;

        public void SetCurrentMoveDirection(float currentMoveDirection)
        {
            _currentMoveDirection = currentMoveDirection;
        }

        private void Update()
        {
            Vector3 currentVelocity = _rigidbody.velocity;
            currentVelocity.x = _currentMoveDirection * _moveSpeed;
            _rigidbody.velocity = currentVelocity;
        }
    }
}