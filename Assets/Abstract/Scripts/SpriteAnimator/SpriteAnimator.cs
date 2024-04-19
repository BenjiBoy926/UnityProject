using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Abstract
{
    public class SpriteAnimator : MonoBehaviour
    {
        public event Action StartedAnimation = delegate { };
        public event Action FinishedAnimation = delegate { };
        public event Action ActionFrameEntered = delegate { };

        private bool IsCurrentFrameFinished => TimeSinceCurrentFrameStart >= CurrentFrameDuration;
        private float CurrentFrameDuration => CurrentFrame.GetDuration(IsTransitioning());
        private float TimeSinceCurrentFrameStart => Time.time - _currentFrameStartTime;
        public float CurrentFrameProgress => TimeSinceCurrentFrameStart / CurrentFrame.Duration;
        public bool FlipX => _body.FlipX;
        private SpriteAnimationFrame CurrentFrame => _currentAnimation.GetFrame(_currentFrameIndex);
        private SpriteAnimationFrame PreviousFrame => _currentAnimation.GetFrame(_currentFrameIndex + 1);
        private SpriteAnimationFrame NextFrame => _currentAnimation.GetFrame(_currentFrameIndex - 1);
        public bool IsCurrentFrameActionFrame => CurrentFrame.IsActionFrame;
        public bool IsPreviousFrameActionFrame => PreviousFrame.IsActionFrame;
        public bool IsNextFrameActionFrame => NextFrame.IsActionFrame;
        public bool IsCurrentFrameFirstFrame => _isFirstFrame;
        
        [SerializeField]
        private SpriteBody _body;
        [SerializeField]
        private SpriteAnimation _currentAnimation;
        [SerializeField]
        private int _currentFrameIndex;
        private float _currentFrameStartTime;
        private SpriteAnimation _nextAnimation;
        private bool _nextFlipX;
        private bool _isFirstFrame = true;

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
            _currentFrameIndex = 0;
            _nextAnimation = null;
            _isFirstFrame = true;
            UpdateSpriteBody();
            StartedAnimation();
        }
        public void TransitionTo(SpriteAnimation animation)
        {
            _nextAnimation = animation;
        }
        public void TransitionFlipX(bool flipX)
        {
            _nextFlipX = flipX;
        }
        public bool IsAnimating(SpriteAnimation animation)
        {
            return _currentAnimation == animation;
        }

        private bool ReadyToTransitionToNextAnimation()
        {
            return IsTransitioningOnCurrentFrame() && IsCurrentFrameFinished;
        }
        private bool ReadyToAdvanceOneFrame()
        {
            return !IsTransitioningOnCurrentFrame() && IsCurrentFrameFinished;
        }
        public bool IsTransitioningOnCurrentFrame()
        {
            return IsTransitioning() && CurrentFrame.IsTransitionFrame;
        }
        private bool IsTransitioning()
        {
            return _nextAnimation != null;
        }

        private void AdvanceOneFrame()
        {
            bool isLastFrame = _currentAnimation.IsLastFrame(_currentFrameIndex);
            _currentFrameIndex++;
            _isFirstFrame = false;
            UpdateSpriteBody();
            if (isLastFrame)
            {
                FinishedAnimation();
            }
        }
        private void UpdateSpriteBody()
        {
            _body.ShowFrame(CurrentFrame);
            _currentFrameStartTime = Time.time;
            if (IsCurrentFrameActionFrame)
            {
                ActionFrameEntered();
            }
        }
    }
}