using NaughtyAttributes;
using System;
using UnityEngine;

namespace AbstractHumanoidToy
{
    [Serializable]
    public class SpriteAnimationFrame
    {
        public const string SpriteRelativePath = nameof(_sprite);

        public Sprite Sprite => _sprite;
        public float Duration => _duration;
        public bool IsSmoothStopFrame => _isSmoothStopFrame;
        public float SmoothStopDuration => _smoothStopDuration;
        public bool IsActionFrame => _isActionFrame;

        [SerializeField]
        private Sprite _sprite;
        [SerializeField]
        private float _duration;
        [SerializeField]
        private bool _isSmoothStopFrame;
        [SerializeField, ShowIf(nameof(_isSmoothStopFrame)), AllowNesting]
        private float _smoothStopDuration;
        [SerializeField]
        private bool _isActionFrame;
    }
}