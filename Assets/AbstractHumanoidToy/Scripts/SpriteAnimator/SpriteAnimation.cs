using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbstractHumanoidToy
{
    [CreateAssetMenu(menuName = nameof(AbstractHumanoidToy) + "/" + nameof(SpriteAnimation))]
    public class SpriteAnimation : ScriptableObject
    {
        [SerializeField]
        private SpriteAnimationFrame[] _frames;
    }
}