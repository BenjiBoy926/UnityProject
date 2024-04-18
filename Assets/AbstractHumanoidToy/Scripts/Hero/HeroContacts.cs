using NaughtyAttributes;
using System;
using UnityEngine;

namespace AbstractHumanoidToy
{
    public class HeroContacts : MonoBehaviour
    {
        private Vector2 OverlapMin => (Vector2)_collider.bounds.min + (Vector2.down * (_overlapAreaSize + _overlapAreaMargin));
        private Vector2 OverlapMax => (Vector2)_collider.bounds.max + (Vector2.down * (_collider.bounds.size.y + _overlapAreaMargin));
        public bool IsOnGround => _isOnGround;

        [SerializeField]
        private Collider2D _collider;
        [SerializeField]
        private float _overlapAreaSize = 0.01f;
        [SerializeField]
        private float _overlapAreaMargin = 0.001f;
        [SerializeField, ReadOnly]
        private bool _isOnGround;

        private void Reset()
        {
            _collider = GetComponentInChildren<Collider2D>();
        }
        private void Update()
        {
            bool overlap = Physics2D.OverlapArea(OverlapMin, OverlapMax);
            SetIsOnGround(overlap);
        }
        private void OnDrawGizmosSelected()
        {
            if (_collider == null)
            {
                return;
            }
            Vector2 size = OverlapMax - OverlapMin;
            Vector2 extents = size / 2;
            Vector2 center = OverlapMin + extents;
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(center, size);
        }
        private void SetIsOnGround(bool isOnGround)
        {
            if (_isOnGround == isOnGround)
            {
                return;
            }
            _isOnGround = isOnGround;
        }
    }
}