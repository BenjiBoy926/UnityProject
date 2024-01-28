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
            _self.AnimateTo(position);
        }
        public Vector2Int ClampInMovement(Vector2Int position)
        {
            Vector2Int delta = position - _self.GridPosition;
            delta = ClampSum(delta, _stats.Movement);
            return _self.GridPosition + delta;
        }
        private Vector2Int ClampSum(Vector2Int vector, int maxSum)
        {
            while (Sum(vector) > maxSum)
            {

            }
            return vector;
        }
        private int Sum(Vector2Int vector)
        {
            return vector.x + vector.y;
        }
        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(WorldPosition, _stats.Movement);
        }
    }
}