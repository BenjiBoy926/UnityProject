using System;
using UnityEngine;

namespace AbstractHumanoidToy
{
    [Serializable]
    public class SpriteAnimationFramePart
    {
        [SerializeField]
        private SpriteAnimationFramePartID _id;
        [SerializeField]
        private Sprite _sprite;
    }
}