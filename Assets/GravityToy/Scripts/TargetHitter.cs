using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GravityToy
{
    public class TargetHitter : MonoBehaviour
    {
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
                t.Hit();
            }
        }
    }
}
