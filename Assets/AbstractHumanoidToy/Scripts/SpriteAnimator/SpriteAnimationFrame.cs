using System;
using UnityEngine;

namespace AbstractHumanoidToy
{
    [Serializable]
    public class SpriteAnimationFrame
    {
        [SerializeField]
        private SpriteAnimationFramePart[] _parts;
        [SerializeField]
        private float _duration;
    }
}