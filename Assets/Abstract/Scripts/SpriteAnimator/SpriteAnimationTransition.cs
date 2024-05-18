using System;
using UnityEngine;

namespace Abstract
{
    [Serializable]
    public struct SpriteAnimationTransition
    {
        public static readonly SpriteAnimationTransition Empty = new SpriteAnimationTransition();
        public readonly bool IsEmpty => _animation == null;
        public readonly SpriteAnimation Animation => _animation;
        public readonly float Scale => _scale;
        public readonly bool FlipX => _flip.X;

        [SerializeField]
        private SpriteAnimation _animation;
        [SerializeField]
        private float _scale;
        [SerializeField]
        private SpriteAnimationFlip _flip;
    
        public SpriteAnimationTransition(SpriteAnimation animation, float scale, SpriteAnimationFlip flip)
        {
            _animation = animation;
            _scale = scale;
            _flip = flip;
        }
        public readonly void ApplyFlip(SpriteBody body)
        {
            _flip.Apply(body);
        }
    }
}