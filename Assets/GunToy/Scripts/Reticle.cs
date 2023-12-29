using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GunToy
{
    public class Reticle : MonoBehaviour
    {
        public void SetPoint(Vector2 point)
        {
            Vector2 toPoint = point - (Vector2)transform.position;
            SetDirection(toPoint);
        }
        public void SetDirection(Vector2 direction)
        {
            transform.up = direction;
        }
    }
}
