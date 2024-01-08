using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

namespace GravityToy
{
    public class Target : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody2D _body;
        [SerializeField]
        private float _speed = 1;

        private void Awake()
        {
            _body.velocity = GetStartingDirection() * _speed;
        }
        private Vector2 GetStartingDirection()
        {
            Vector2 toCenter = -transform.position;
            if (Mathf.Abs(toCenter.x) > Mathf.Abs(toCenter.y))
            {
                toCenter.x = Mathf.Sign(toCenter.x);
                toCenter.y = 0;
            }
            else
            {
                toCenter.x = 0;
                toCenter.y = Mathf.Sign(toCenter.y);
            }
            return toCenter;
        }
        public void Hit()
        {
            Destroy(gameObject);
        }
    }
}
