using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbstractHumanoidToy
{
    public class SpriteAnimator : MonoBehaviour
    {
        private SpriteAnimationFrame CurrentAnimationFrame => _currentAnimation.GetFrame(_currentFrame);
        
        [SerializeField]
        private SpriteBody _body;
        [SerializeField]
        private SpriteAnimation _currentAnimation;
        [SerializeField]
        private int _currentFrame;

        private void Reset()
        {
            _body = GetComponent<SpriteBody>();
        }
        private void OnValidate()
        {
            if (_body == null || _currentAnimation == null)
            {
                return;
            }
            ValidateCurrentFrame();
            ShowCurrentFrame();
        }

        private void ValidateCurrentFrame()
        {
            _currentFrame %= _currentAnimation.FrameCount;
        }
        private void ShowCurrentFrame()
        {
            _body.ShowFrame(CurrentAnimationFrame);
        }
    }
}