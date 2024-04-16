using NaughtyAttributes;
using System;
using UnityEngine;

namespace AbstractHumanoidToy
{
    [Serializable]
    public class SpriteAnimationFrame
    {
        public float Duration => _duration;
        public bool IsSmoothStopFrame => _isSmoothStopFrame;
        public float SmoothStopDuration => _smoothStopDuration;
        public bool IsActionFrame => _isActionFrame;

        [SerializeField]
        private SpriteAnimationFramePart[] _parts;
        [SerializeField]
        private float _duration;
        [SerializeField]
        private bool _isSmoothStopFrame;
        [SerializeField, ShowIf(nameof(_isSmoothStopFrame)), AllowNesting]
        private float _smoothStopDuration;
        [SerializeField]
        private bool _isActionFrame;

        internal SpriteAnimationFramePart FindPart(SpriteBodyPartID id)
        {
            bool PartHasID(SpriteAnimationFramePart part) => part.HasID(id);
            return Array.Find(_parts, PartHasID);
        }
    }
}