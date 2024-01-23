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
        public bool IsPositionInMovementRange(Vector2Int position)
        {
            Vector2Int delta = position - _self.GridPosition;
            return Mathf.Abs(delta.x) + Mathf.Abs(delta.y) <= _stats.Movement;
        }
    }
}