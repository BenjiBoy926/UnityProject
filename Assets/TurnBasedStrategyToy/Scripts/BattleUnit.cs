using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace TurnBasedStrategyToy
{
    public class BattleUnit : MonoBehaviour
    {
        public Vector3 WorldPosition => _self.WorldPosition;

        [SerializeField]
        private BattleUnitStats _stats;
        [SerializeField]
        private ObjectOnGrid _self;

        public void AnimateTo(Vector2Int position)
        {
            position = ClampInMovement(position);
            _self.AnimateTo(position);
        }
        private void MovementReachableTiles(List<Vector2Int> tiles)
        {
            Assert.IsNotNull(tiles);

            tiles.Clear();
            for (int x = -_stats.Movement; x <= _stats.Movement; x++)
            {
                for (int y = -_stats.Movement; y <= _stats.Movement; y++)
                {
                    Vector2Int delta = new Vector2Int(x, y);
                    if (AbsoluteSum(delta) > _stats.Movement)
                    {
                        continue;
                    }
                    Vector2Int position = _self.GridPosition + delta;
                    tiles.Add(position);
                }
            }
        }
        public Vector2Int ClampInMovement(Vector2Int position)
        {
            Vector2Int delta = position - _self.GridPosition;
            delta = ClampAbsoluteSum(delta, _stats.Movement);
            return _self.GridPosition + delta;
        }
        private Vector2Int ClampAbsoluteSum(Vector2Int vector, int maxSum)
        {
            if (AbsoluteSum(vector) <= maxSum)
            {
                return vector;
            }
            return vector;
        }
        private int AbsoluteSum(Vector2Int vector)
        {
            return Mathf.Abs(vector.x) + Mathf.Abs(vector.y);
        }
        private void OnDrawGizmosSelected()
        {
            List<Vector2Int> reachable = new List<Vector2Int>();
            MovementReachableTiles(reachable);
            foreach (Vector2Int position in reachable)
            {
                Gizmos.DrawWireCube(_self.GridToWorld(position), Vector3.one);
            }
        }
    }
}