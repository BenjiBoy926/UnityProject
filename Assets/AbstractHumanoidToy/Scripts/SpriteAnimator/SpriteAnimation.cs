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

        public SpriteAnimationFrame GetFrame(int frameIndex)
        {
            return _frames[MakeIndexValid(frameIndex)];
        }
        private int MakeIndexValid(int index)
        {
            int remainder = index % FrameCount;
            if (index < 0)
            {
                return (remainder + FrameCount) % FrameCount;
            }
            else
            {
                return remainder;
            }
        }
    }
}