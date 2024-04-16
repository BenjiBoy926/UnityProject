using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbstractHumanoidToy
{
    public class SpriteAnimator : MonoBehaviour
    {
        private SpriteAnimationFrame CurrentAnimationFrame => _currentAnimation.GetFrame(_currentFrame);
        private float TimeSinceCurrentFrameStart => Time.time - _currentFrameStartTime;
        
        [SerializeField]
        private SpriteBody _body;
        [SerializeField]
        private SpriteAnimation _currentAnimation;
        [SerializeField]
        private int _currentFrame;
        private float _currentFrameStartTime;

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
            UpdateSpriteBody();
        }
        private void OnEnable()
        {
            UpdateSpriteBody();
        }
        private void Update()
        {
            if (ReadyToAdvanceOneFrame())
            {
                AdvanceOneFrame();
            }
        }

        private bool ReadyToAdvanceOneFrame()
        {
            return TimeSinceCurrentFrameStart >= CurrentAnimationFrame.Duration;
        }
        private void AdvanceOneFrame()
        {
            _currentFrame++;
            UpdateSpriteBody();
        }

        private void UpdateSpriteBody()
        {
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
            _currentFrameStartTime = Time.time;
        }
    }
}