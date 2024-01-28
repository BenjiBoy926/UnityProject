using System.Collections;
using UnityEngine;
using NaughtyAttributes;

namespace TurnBasedStrategyToy
{
    public class Cursor : MonoBehaviour
    {
        [SerializeField]
        private ObjectOnGrid _self;
        [SerializeField]
        private BattleUnit _target;
        [SerializeField]
        private LineRenderer _moveLine;
        private Vector3[] _linePositions = new Vector3[2];

        private void Update()
        {
            if (_target == null)
            {
                _moveLine.enabled = false;
                return;
            }
            Vector2Int intendedNextPosition = _target.ClampInMovement(_self.GridPosition);
            _moveLine.enabled = true;
            _linePositions[0] = _target.WorldPosition;
            _linePositions[1] = _self.GridToWorld(intendedNextPosition);
            _moveLine.SetPositions(_linePositions);
        }

        public void SnapToGridPointClosestToScreenPoint(Vector2 screen)
        {
            _self.SnapToGridPointClosestToScreenPoint(screen);
        }
        public void Select()
        {
            _target = ChooseOccupant(_self.GridPosition);
        }
        public void Deselect()
        {
            _target = null;
        }
        private BattleUnit ChooseOccupant(Vector2Int position)
        {
            foreach (ObjectOnGrid obj in _self.OccupantsOf(position))
            {
                if (obj == _self)
                {
                    continue;
                }
                if (obj.TryGetComponent(out BattleUnit unit))
                {
                    return unit;
                }
            }
            return null;
        }

        [Button("Animate Target to Self", EButtonEnableMode.Playmode)]
        public void AnimateTargetToSelf()
        {
            if (_target == null)
            {
                return;
            }
            _target.AnimateTo(_self.GridPosition);
        }
    }
}