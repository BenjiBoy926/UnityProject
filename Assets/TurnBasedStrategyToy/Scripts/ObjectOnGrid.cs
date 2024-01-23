using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

namespace TurnBasedStrategyToy
{
    public class ObjectOnGrid : MonoBehaviour
    {
        private Vector3 IntendedWorldPosition => _grid.GridToWorld(_position);
        public Vector2Int Position => _position;

        [SerializeField]
        private Grid _grid;
        [SerializeField]
        private Vector2Int _position;
        [SerializeField]
        private Ease _moveEase = Ease.OutQuint;
        [SerializeField]
        private float _moveDuration = 1;

        private void OnValidate()
        {
            if (Application.isPlaying)
            {
                AnimateToCurrentPosition();
            }
            else
            {
                SnapToCurrentPosition();
            }
        }
        private void Awake()
        {
            _grid.Register(this);
        }
        private void OnDestroy()
        {
            _grid.Unregister(this);
        }

        public IEnumerable<ObjectOnGrid> OccupantsOf(Vector2Int position)
        {
            return _grid.OccupantsOf(position);
        }
        public void AnimateTo(Vector2Int position)
        {
            if (position == _position)
            {
                return;
            }
            _position = position;
            AnimateToCurrentPosition();
        }
        private void AnimateToCurrentPosition()
        {
            transform.DOKill();
            transform.DOMove(IntendedWorldPosition, _moveDuration).SetEase(_moveEase);
        }

        public void SnapToGridPointClosestToScreenPoint(Vector2 screen)
        {
            SnapTo(_grid.ScreenToGrid(screen));
        }
        internal void SnapTo(Vector2Int position)
        {
            if (position == _position)
            {
                return;
            }
            _position = position;
            SnapToCurrentPosition();
        }
        private void SnapToCurrentPosition()
        {
            transform.position = IntendedWorldPosition;
        }
    }
}
