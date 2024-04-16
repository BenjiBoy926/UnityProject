using System;
using UnityEngine;

namespace AbstractHumanoidToy
{
    public class SpriteBody : MonoBehaviour
    {
        [SerializeField]
        private SpriteBodyPart[] _parts;

        private void Reset()
        {
            _parts = GetComponentsInChildren<SpriteBodyPart>();
        }

        public void ShowFrame(SpriteAnimationFrame frame)
        {
            for (int i = 0; i < _parts.Length; i++)
            {
                _parts[i].ShowFrame(frame);
            }
        }
        public void SetFlipX(bool flipX)
        {
            for (int i = 0; i < _parts.Length; i++)
            {
                _parts[i].SetFlipX(flipX);
            }
        }
    }
}