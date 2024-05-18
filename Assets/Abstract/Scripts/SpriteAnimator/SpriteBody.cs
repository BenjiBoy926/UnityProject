using System;
using UnityEngine;

namespace Abstract
{
    public class SpriteBody : MonoBehaviour
    {
        public bool FlipX => _renderer.flipX;
        public bool FlipY => _renderer.flipY;  

        [SerializeField]
        private SpriteRenderer _renderer;

        private void Reset()
        {
            _renderer = GetComponent<SpriteRenderer>();
        }

        public void ShowFrame(SpriteAnimationFrame frame)
        {
            _renderer.sprite = frame.Sprite;
        }
        public void SetFlip(bool x, bool y)
        {
            _renderer.flipX = x;
            _renderer.flipY = y;
        }
        public void SetFlipX(bool x)
        {
            _renderer.flipX = x;
        }
    }
}