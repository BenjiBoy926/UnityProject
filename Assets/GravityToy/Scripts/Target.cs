using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

namespace GravityToy
{
    public class Target : MonoBehaviour
    {
        [Button("Hit Test")]
        private void HitTest()
        {
            Hit(0);
        }
        public void Hit(float hitterVelocity)
        {
            Destroy(gameObject);
        }
    }
}
