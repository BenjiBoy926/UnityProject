using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace TurnBasedStrategyToy
{
    public class ObjectOnGrid : MonoBehaviour
    {
        private Vector3 IntendedWorldPosition => GridPositionToWorldPosition(_position);

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
                AnimateToPosition();
            }
            else
            {
                SnapToPosition();
            }
        }
        private void AnimateToPosition()
        {
            transform.DOKill();
            transform.DOMove(IntendedWorldPosition, _moveDuration);
        }
        private void SnapToPosition()
        {
            transform.position = IntendedWorldPosition;
        }
        private static Vector3 GridPositionToWorldPosition(Vector2Int gridPosition)
        {
            return new Vector3(gridPosition.x, gridPosition.y, 0);
        }
    }
}
