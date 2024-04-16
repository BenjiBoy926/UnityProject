using System;
using UnityEngine;

namespace AbstractHumanoidToy
{
    [Serializable]
    public class SpriteAnimationFramePart
    {
        public Sprite Sprite => _sprite;

        [SerializeField]
        private SpriteBodyPartID _id;
        [SerializeField]
        private Sprite _sprite;

        public bool HasID(SpriteBodyPartID id)
        {
            return _id == id;
        }
    }
}