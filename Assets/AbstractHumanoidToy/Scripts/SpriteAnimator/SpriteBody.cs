using System;
using UnityEngine;

namespace AbstractHumanoidToy
{
    public class SpriteBody : MonoBehaviour
    {
        public bool FlipX => _flipX;

        [SerializeField]
        private SpriteBodyPart[] _parts;
        [SerializeField]
        private bool _flipX;

        private void OnValidate()
        {
            if (_parts == null)
            {
                return;
            }
            ShowFlipX();
        }
        private void Reset()
        {
            _parts = GetComponentsInChildren<SpriteBodyPart>();
        }
        private void OnEnable()
        {
            ShowFlipX();
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
            if (_flipX == flipX)
            {
                return;
            }
            _flipX = flipX;
            ShowFlipX();
        }
        private void ShowFlipX()
        {
            for (int i = 0; i < _parts.Length; i++)
            {
                _parts[i].SetFlipX(_flipX);
            }
        }
    }
}