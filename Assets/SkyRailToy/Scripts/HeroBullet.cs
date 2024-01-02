using System.Collections;
using UnityEngine;

namespace SkyRailToy
{
    public class HeroBullet : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody2D _rigidbody;

        public void Send(float speed)
        {
            _rigidbody.velocity = transform.right * speed;
        }
    }
}