using System.Collections;
using UnityEngine;

namespace GravityToy
{
    public struct BoundaryEdge
    {
        private Vector2 _min;
        private Vector2 _max;

        public BoundaryEdge(Vector2 min, Vector2 max)
        {
            _min = min;
            _max = max;
        }

        public Vector2 GetRandomPointOnEdge()
        {
            return Vector2.Lerp(_min, _max, Random.Range(0f, 1f));
        }
    }
}