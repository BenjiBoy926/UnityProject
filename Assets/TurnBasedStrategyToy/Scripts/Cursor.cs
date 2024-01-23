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

        [Button("Snap Target to Self")]
        public void SnapTargetToSelf()
        {
            _target.SnapTo(_self.Position);
        }
        [Button("Animate Target to Self", EButtonEnableMode.Playmode)]
        public void AnimateTargetToSelf()
        {
            _target.AnimateTo(_self.Position);
        }
    }
}