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
        private ObjectOnGrid _target;

        public void SnapToGridPointClosestToScreenPoint(Vector2 screen)
        {
            _self.SnapToGridPointClosestToScreenPoint(screen);
        }
        public void Select()
        {
            _target = ChooseOccupant(_self.Position);
        }
        public void Deselect()
        {
            _target = null;
        }
        private ObjectOnGrid ChooseOccupant(Vector2Int position)
        {
            foreach (ObjectOnGrid obj in _self.OccupantsOf(position))
            {
                if (obj == _self)
                {
                    continue;
                }
                return obj;
            }
            return null;
        }

        [Button("Snap Target to Self")]
        public void SnapTargetToSelf()
        {
            if (_target == null)
            {
                return;
            }
            _target.SnapTo(_self.Position);
        }
        [Button("Animate Target to Self", EButtonEnableMode.Playmode)]
        public void AnimateTargetToSelf()
        {
            if (_target == null)
            {
                return;
            }
            _target.AnimateTo(_self.Position);
        }
    }
}