using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurnBasedStrategyToy
{
    public class ObjectOnGrid : MonoBehaviour
    {
        [SerializeField]
        private Vector2Int _position;

        private void OnValidate()
        {
            SnapToPosition();
        }
        private void SnapToPosition()
        {
            transform.position = GridPositionToWorldPosition(_position);
        }
        private static Vector3 GridPositionToWorldPosition(Vector2Int gridPosition)
        {
            return new Vector3(gridPosition.x, gridPosition.y, 0);
        }
    }
}
