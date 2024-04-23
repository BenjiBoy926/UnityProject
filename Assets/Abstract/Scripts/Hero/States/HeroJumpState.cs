using UnityEngine;

namespace Abstract
{
    public class HeroJumpState : HeroState
    {
        private float TimeSinceJumpActionFrameStart => Time.time - _timeOfJumpActionFrameStart;
        private float JumpProgress => TimeSinceJumpActionFrameStart / Hero.MaxJumpTime;

        private float _timeOfJumpActionFrameStart = -1;

        public HeroJumpState(Hero hero) : base(hero) { }

        public override void Enter()
        {
            base.Enter();
            Hero.TransitionToJumpAnimation(0.1f);
        }

        public override void Update(float dt)
        {
            base.Update(dt);
            if (!Hero.IsAnimatingJump)
            {
                return;
            }
            if (StartedJumpActionThisFrame())
            {
                _timeOfJumpActionFrameStart = Time.time;
            }
            if (Hero.IsCurrentFrameActionFrame)
            {
                Hero.SetVerticalVelocity(GetJumpSpeed());
            }
            if (ShouldTransitionOutOfJump())
            {
                Hero.SetState(new HeroFreeFallState(Hero));
            }
            Hero.ApplyJumpAirControl();
        }
        private bool ShouldTransitionOutOfJump()
        {
            return IsHeroAnimatingJumpActionFrame() && ((!Hero.IsJumping && TimeSinceJumpActionFrameStart >= Hero.MinJumpTime) || (TimeSinceJumpActionFrameStart >= Hero.MaxJumpTime));
        }
        private float GetJumpSpeed()
        {
            return Hero.EvaluateJumpSpeedCurve(JumpProgress) * Hero.MaxJumpSpeed;
        }
        private bool StartedJumpActionThisFrame()
        {
            return IsHeroAnimatingJumpActionFrame() && !IsTimeOfJumpActionStartSet();
        }
        private bool IsHeroAnimatingJumpActionFrame()
        {
            return Hero.IsAnimatingJump && Hero.IsCurrentFrameActionFrame;
        }
        private bool IsTimeOfJumpActionStartSet()
        {
            return _timeOfJumpActionFrameStart >= 0;
        }
    }
}