using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbstractHumanoidToy
{
    public class SpriteAnimator : MonoBehaviour
    {
        public event Action ActionFrameEntered = delegate { };

        private SpriteAnimationFrame CurrentFrame => _currentAnimation.GetFrame(_currentFrameIndex);
        private int PreviousFrameIndex => RepeatFrameIndex(_currentFrameIndex - 1);
        private SpriteAnimationFrame PreviousFrame => _currentAnimation.GetFrame(PreviousFrameIndex);
        private float TimeSinceCurrentFrameStart => Time.time - _currentFrameStartTime;
        public bool IsCurrentFrameActionFrame => CurrentFrame.IsActionFrame;
        public bool IsPreviousFrameActionFrame => PreviousFrame.IsActionFrame;
        private int FrameCount => _currentAnimation.FrameCount;
        public float CurrentFrameProgress => TimeSinceCurrentFrameStart / CurrentFrame.Duration;
        public bool FlipX => _body.FlipX;
        
        [SerializeField]
        private SpriteBody _body;
        [SerializeField]
        private SpriteAnimation _currentAnimation;
        [SerializeField]
        private int _currentFrameIndex;
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
            _currentFrameIndex = 0;
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
        public bool IsAnimating(SpriteAnimation animation)
        {
            return _currentAnimation == animation;
        }

        private bool ReadyToTransitionToNextAnimation()
        {
            return ShouldSmoothStopOnCurrentFrame() && TimeSinceCurrentFrameStart >= CurrentFrame.SmoothStopDuration;
        }
        private bool ReadyToAdvanceOneFrame()
        {
            return !ShouldSmoothStopOnCurrentFrame() && TimeSinceCurrentFrameStart >= CurrentFrame.Duration;
        }
        private bool ShouldSmoothStopOnCurrentFrame()
        {
            return HasAnimationToTransitionTo() && CurrentFrame.IsSmoothStopFrame;
        }
        private bool HasAnimationToTransitionTo()
        {
            return _nextAnimation != null;
        }

        private void AdvanceOneFrame()
        {
            _currentFrameIndex++;
            UpdateSpriteBody();
        }
        private void UpdateSpriteBody()
        {
            _currentFrameIndex = RepeatFrameIndex(_currentFrameIndex);
            ShowCurrentFrame();
            RaiseNewFrameEvents();
        }
        private int RepeatFrameIndex(int index)
        {
            int remainder = index % _currentAnimation.FrameCount;
            if (index < 0)
            {
                return (remainder + FrameCount) % FrameCount;
            }
            else
            {
                return remainder;
            }
        }
        private void ShowCurrentFrame()
        {
            _body.ShowFrame(CurrentFrame);
            _currentFrameStartTime = Time.time;
        }
        private void RaiseNewFrameEvents()
        {
            if (CurrentFrame.IsActionFrame)
            {
                ActionFrameEntered();
            }
        }
    }
}