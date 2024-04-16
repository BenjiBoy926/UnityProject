using System;
using UnityEngine;

namespace AbstractHumanoidToy
{
    [Serializable]
    public class SpriteAnimationFramePart
    {
        [SerializeField]
        private SpriteBodyPartID _id;
        [SerializeField]
        private Sprite _sprite;
    }
}