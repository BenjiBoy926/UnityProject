using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbstractHumanoidToy
{
    public class SpriteAnimator : MonoBehaviour
    {
        public event Action ActionFrameEntered = delegate { };

        private SpriteAnimationFrame CurrentAnimationFrame => _currentAnimation.GetFrame(_currentFrame);
        private float TimeSinceCurrentFrameStart => Time.time - _currentFrameStartTime;
        public float CurrentFrameDuration => CurrentAnimationFrame.Duration;
        
        [SerializeField]
        private SpriteBody _body;
        [SerializeField]
        private SpriteAnimation _currentAnimation;
        [SerializeField]
        private int _currentFrame;
        private float _currentFrameStartTime;
        private SpriteAnimation _nextAnimation;
        private bool _nextFlipX;

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
            if (ReadyToTransitionToNextAnimation())
            {
                SetNextAnimation();
            }
            if (ReadyToAdvanceOneFrame())
            {
                AdvanceOneFrame();
            }
        }

        private void SetNextAnimation()
        {
            SetAnimation(_nextAnimation);
            _body.SetFlipX(_nextFlipX);
        }
        public void SetAnimation(SpriteAnimation animation)
        {
            _currentAnimation = animation;
            _currentFrame = 0;
            _nextAnimation = null;
            UpdateSpriteBody();
        }
        public void TransitionTo(SpriteAnimation animation)
        {
            _nextAnimation = animation;
        }
        public void TransitionFlipX(bool flipX)
        {
            _nextFlipX = flipX;
        }

        private bool ReadyToTransitionToNextAnimation()
        {
            return ShouldSmoothStopOnCurrentFrame() && TimeSinceCurrentFrameStart >= CurrentAnimationFrame.SmoothStopDuration;
        }
        private bool ReadyToAdvanceOneFrame()
        {
            return !ShouldSmoothStopOnCurrentFrame() && TimeSinceCurrentFrameStart >= CurrentAnimationFrame.Duration;
        }
        private bool ShouldSmoothStopOnCurrentFrame()
        {
            return HasAnimationToTransitionTo() && CurrentAnimationFrame.IsSmoothStopFrame;
        }
        private bool HasAnimationToTransitionTo()
        {
            return _nextAnimation != null;
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
            RaiseNewFrameEvents();
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
        private void RaiseNewFrameEvents()
        {
            if (CurrentAnimationFrame.IsActionFrame)
            {
                ActionFrameEntered();
            }
        }
    }
}