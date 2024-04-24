using Core;
using NaughtyAttributes;
using System;
using UnityEngine;

namespace Abstract
{
    public class HeroContacts : MonoBehaviour
    {
        private Vector2 ColliderCenter => _collider.bounds.center;
        private Vector2 ColliderExtents => _collider.bounds.extents;
        private Vector2 ColliderSize => _collider.bounds.size;
        private Vector2 OverlapAreaExtentsVector => Vector2.one * _overlapAreaExtents;
        private Vector2 OverlapAreaMarginVector => Vector2.one * _overlapAreaMargin;
        private Vector2 OverlapAreaSizeVector => OverlapAreaExtentsVector * 2;
        public bool IsTouchingGround => _isTouchingGround;

        [SerializeField]
        private Collider2D _collider;
        [SerializeField]
        private float _overlapAreaExtents = 0.01f;
        [SerializeField]
        private float _overlapAreaMargin = 0.001f;
        [SerializeField, ReadOnly]
        private bool _isTouchingGround;

        private void Reset()
        {
            _collider = GetComponentInChildren<Collider2D>();
        }
        private void OnDrawGizmosSelected()
        {
            DrawOverlapAreaGizmo(Vector2.up);
            DrawOverlapAreaGizmo(Vector2.down);
            DrawOverlapAreaGizmo(Vector2.left);
            DrawOverlapAreaGizmo(Vector2.right);
        }
        private void DrawOverlapAreaGizmo(Vector2 direction)
        {
            Gizmos.DrawWireCube(GetAreaCenter(direction), GetAreaSize(direction));
        }

        private void Update()
        {

        }

        private Vector2 GetAreaCenter(Vector2 direction)
        {
            direction = (OverlapAreaExtentsVector + OverlapAreaMarginVector + ColliderExtents) * direction;
            return ColliderCenter + direction;
        }
        private Vector2 GetAreaSize(Vector2 direction)
        {
            direction = direction.Abs();
            Vector2 parallelSize = OverlapAreaSizeVector;
            Vector2 perpendicularSize = ColliderSize - 2 * _overlapAreaMargin * Vector2.one;
            return parallelSize * direction + perpendicularSize * direction.Flipped();
        }
    }
}