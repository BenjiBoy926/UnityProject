using System.Collections;
using UnityEngine;

namespace SkyRailToy
{
    public class Hero : MonoBehaviour
    {
        [SerializeField]
        private HeroStats _stats;
        [SerializeField]
        private Rigidbody2D _rigidbody;
        [SerializeField]
        private int _lateralDirection;
        [SerializeField]
        private bool _isJumping;
        private float _timeOfJumpStart;

        private void Update()
        {
            UpdateLateralMovement();
            UpdateJump();
        }
        private void UpdateLateralMovement()
        {
            Vector2 currentVelocity = _rigidbody.velocity;
            currentVelocity.x = _lateralDirection * _stats.MoveSpeed;
            _rigidbody.velocity = currentVelocity;
        }
        private void UpdateJump()
        {

        }
    }
}