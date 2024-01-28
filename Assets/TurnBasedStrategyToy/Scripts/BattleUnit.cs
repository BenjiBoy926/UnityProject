using System.Collections;
using UnityEngine;

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
            Gizmos.DrawWireSphere(WorldPosition, _stats.Movement);
        }
    }
}