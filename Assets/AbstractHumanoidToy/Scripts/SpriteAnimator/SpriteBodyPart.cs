using System;
using UnityEngine;

namespace AbstractHumanoidToy
{
    public class SpriteBodyPart : MonoBehaviour
    {
        [SerializeField]
        private SpriteBodyPartID _id;
        [SerializeField]
        private SpriteRenderer _renderer;

        private void Reset()
        {
            _renderer = GetComponent<SpriteRenderer>();
        }

        internal void ShowFrame(SpriteAnimationFrame frame)
        {
            SpriteAnimationFramePart part = frame.FindPart(_id);
            if (part != null)
            {
                _renderer.sprite = part.Sprite;
            }
        }
    }
}