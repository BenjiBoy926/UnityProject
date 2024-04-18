using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbstractHumanoidToy
{
    [CreateAssetMenu(menuName = nameof(AbstractHumanoidToy) + "/" + nameof(SpriteAnimation))]
    public class SpriteAnimation : ScriptableObject
    {
        public const string FramesRelativePath = nameof(_frames);

        public int FrameCount => _frames.Length;

        [SerializeField]
        private SpriteAnimationFrame[] _frames;

        internal SpriteAnimationFrame GetFrame(int currentFrame)
        {
            return _frames[currentFrame];
        }
    }
}