using System.Collections;
using System;
using UnityEngine;

namespace GravityToy
{
    public class Boundary : MonoBehaviour
    {
        private BoundaryEdge Left => new BoundaryEdge(Bounds.min, Bounds.min + Vector3.up * Height);
        private BoundaryEdge Right => new BoundaryEdge(Bounds.min + Vector3.right * Width, Bounds.max);
        private BoundaryEdge Top => new BoundaryEdge(Bounds.max + Vector3.left * Width, Bounds.max);
        private BoundaryEdge Bottom => new BoundaryEdge(Bounds.min, Bounds.min + Vector3.right * Width);
        private float Width => Bounds.size.x;
        private float Height => Bounds.size.y;
        private Bounds Bounds => _boundingTrigger.bounds;

        [SerializeField]
        private Collider2D _boundingTrigger; 

        public Vector2 GetRandomPointOnRandomEdge()
        {
            return GetRandomEdge().GetRandomPointOnEdge();
        }
        private BoundaryEdge GetRandomEdge()
        {
            return GetEdge(UnityEngine.Random.Range(0, 4));
        }
        private BoundaryEdge GetEdge(int index)
        {
            return index switch
            {
                0 => Left,
                1 => Top,
                2 => Right,
                3 => Bottom,
                _ => throw new ArgumentOutOfRangeException(nameof(index)),
            };
        }
    }
}