using System.Collections;
using UnityEngine;
using NaughtyAttributes;

namespace GunToy
{
    public class RecoilGun : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody2D _rigidbody;
        [SerializeField]
        private float _speed = 10;

        private void Reset()
        {
            _rigidbody = GetComponentInParent<Rigidbody2D>();
        }

        [Button("Fire")]
        public void Fire()
        {
            Vector2 direction = transform.up;
            _rigidbody.velocity = direction * _speed;
        }
    }
}