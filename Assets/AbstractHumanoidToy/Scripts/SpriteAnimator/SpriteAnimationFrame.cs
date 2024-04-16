using System;
using UnityEngine;

namespace AbstractHumanoidToy
{
    [Serializable]
    public class SpriteAnimationFrame
    {
        public float Duration => _duration;

        [SerializeField]
        private SpriteAnimationFramePart[] _parts;
        [SerializeField]
        private float _duration;

        internal SpriteAnimationFramePart FindPart(SpriteBodyPartID id)
        {
            bool PartHasID(SpriteAnimationFramePart part) => part.HasID(id);
            return Array.Find(_parts, PartHasID);
        }
    }
}