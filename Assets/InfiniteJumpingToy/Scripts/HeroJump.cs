using Core;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InfinitJumpingToy
{
    public class HeroJump : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody2D _body;
        [SerializeField]
        private float _jumpSpeed = 5f;
        [SerializeField]
        private float _maxJumpTime = 1;
        private bool _isJumping = false;
        private Cooldown _jumpCooldown;

        private void OnValidate()
        {
            _jumpCooldown = new Cooldown(_maxJumpTime);
        }
        private void Awake()
        {
            _jumpCooldown = new Cooldown(_maxJumpTime);
        }

        [Button]
        public void StartJumping()
        {
            _isJumping = true;
            _jumpCooldown.Start();
        }
        public void StopJumping()
        {
            _isJumping = false;
        }

        private void Update()
        {
            if (!_isJumping)
            {
                return;
            }
            _body.SetVelocity(_jumpSpeed, Dimension.Y);
            if (_jumpCooldown.IsCooledDown)
            {
                StopJumping();
            }
        }
    }
}
