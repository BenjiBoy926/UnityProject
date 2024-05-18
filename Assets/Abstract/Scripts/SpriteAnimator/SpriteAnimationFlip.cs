using Core;
using System;
using UnityEngine;

namespace Abstract
{
    [Serializable]
    public struct SpriteAnimationFlip
    {
        public readonly bool X => _x;

        [SerializeField]
        private bool _x;
        [SerializeField] 
        private bool _y;
    
        public SpriteAnimationFlip(bool x, bool y)
        {
            _x = x;
            _y = y;
        }

        public readonly void Apply(SpriteBody body)
        {
            body.SetFlip(_x, _y);
        }
    }
}