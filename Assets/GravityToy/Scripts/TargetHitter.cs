using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GravityToy
{
    public class TargetHitter : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out Target t))
            {
                t.Hit(collision.relativeVelocity.magnitude);
            }
        }
    }
}
