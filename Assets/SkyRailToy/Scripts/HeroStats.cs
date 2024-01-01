using System.Collections;
using UnityEngine;

namespace SkyRailToy
{
    [CreateAssetMenu(menuName = "Sky Rail Toy/Hero Stats")]
    public class HeroStats : ScriptableObject
    {
        public float MoveSpeed => _moveSpeed;
        public float JumpSpeed => _jumpSpeed;
        public float MaxJumpTime => _maxJumpTime;

        [SerializeField]
        private float _moveSpeed = 5;
        [SerializeField]
        private float _jumpSpeed = 5;
        [SerializeField]
        private float _maxJumpTime = 1;
    }
}