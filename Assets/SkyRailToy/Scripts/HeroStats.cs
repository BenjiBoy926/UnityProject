using System.Collections;
using UnityEngine;

namespace SkyRailToy
{
    public class HeroStats : MonoBehaviour
    {
        public float MoveSpeed => _moveSpeed;
        public float JumpSpeed => _jumpSpeed;
        public float MaxJumpTime => _maxJumpTime;
        public float FireRate => _fireRate;
        public float BulletSpeed => _bulletSpeed;

        [SerializeField]
        private float _moveSpeed = 5;
        [SerializeField]
        private float _jumpSpeed = 5;
        [SerializeField]
        private float _maxJumpTime = 1;
        [SerializeField]
        private float _fireRate = 0.1f;
        [SerializeField]
        private float _bulletSpeed = 20;
    }
}