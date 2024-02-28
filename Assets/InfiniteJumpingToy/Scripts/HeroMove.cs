using Core;
using UnityEngine;

namespace InfinitJumpingToy
{
    public class HeroMove : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody2D _body;
        [SerializeField]
        private float _speed = 5;
        [SerializeField]
        private float _direction = 0;

        public void SetDirection(float direction)
        {
            _direction = direction;
        }

        private void Reset()
        {
            _body = GetComponentInParent<Rigidbody2D>();
        }
        private void Update()
        {
            _body.SetVelocity(_direction * _speed, Dimension.X);
        }
    }
}