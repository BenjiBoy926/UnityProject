using System;
using UnityEngine;

namespace Abstract
{
    [CreateAssetMenu(menuName = nameof(Abstract) + "/" + nameof(SpriteAnimation))]
    public class SpriteAnimation : ScriptableObject
    {
        public const string FramesRelativePath = nameof(_frames);

        public int FrameCount => _frames.Length;
        public float Duration => TimeBefore(FrameCount - 1) + _frames[^1].Duration;
        public int IndexOfFirstActionFrame => Array.FindIndex(_frames, IsActionFrame);
        public float TimeAfterFirstActionFrame => TimeAfter(IndexOfFirstActionFrame);
        public float TimeUpToAndIncludingFirstActionFrame => TimeBefore(IndexOfFirstActionFrame + 1);

        [SerializeField]
        private SpriteAnimationFrame[] _frames;

        public SpriteAnimationFrame GetFrame(int frameIndex)
        {
            return _frames[MakeIndexValid(frameIndex)];
        }
        public bool IsLastFrame(int index)
        {
            index = MakeIndexValid(index);
            return index == (FrameCount - 1);
        }
        public float TimeBefore(int index)
        {
            index = MakeIndexValid(index);
            float total = 0;
            for (int i = 0; i < index; i++)
            {
                total += _frames[i].Duration;
            }
            return total;
        }
        public float TimeAfter(int index)
        {
            index = MakeIndexValid(index);
            float total = 0;
            for (int i = index + 1; i < FrameCount; i++)
            {
                total += _frames[i].Duration;
            }
            return total;
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
        private bool IsActionFrame(SpriteAnimationFrame frame)
        {
            return frame.IsActionFrame;
        }
    }
}