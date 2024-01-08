using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GravityToy
{
    public class TargetHitter : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody2D _body;
        [SerializeField]
        private float _boost = 1;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            TryToHitTarget(collision.otherCollider);
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            TryToHitTarget(collision);
        }
        private void TryToHitTarget(Collider2D collider)
        {
            if (collider.TryGetComponent(out Target t))
            {
                HitTarget(t);
            }
        }
        private void HitTarget(Target t)
        {
            t.Hit();
            _body.AddForce(_body.velocity.normalized * _boost, ForceMode2D.Impulse);
        }
    }
}
