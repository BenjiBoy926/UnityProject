using UnityEngine;

namespace Abstract
{
    public class HeroJumpState : HeroState
    {
        private float TimeSinceJumpActionFrameStart => Time.time - _timeOfJumpActionFrameStart;

        private float _timeOfJumpActionFrameStart;

        public HeroJumpState(Hero hero) : base(hero) { }

        public override void Enter()
        {
            base.Enter();
            Hero.ActionFrameEntered += OnActionFrameEntered;
            Hero.TransitionToJumpAnimation();
        }
        public override void Exit()
        {
            base.Exit();
            Hero.ActionFrameEntered -= OnActionFrameEntered;
        }

        private void OnActionFrameEntered()
        {
            if (!Hero.IsAnimatingJump)
            {
                return;
            }
            _timeOfJumpActionFrameStart = Time.time;
        }

        public override void Update(float dt)
        {
            base.Update(dt);
            if (!Hero.IsAnimatingJump)
            {
                return;
            }
            if (Hero.IsCurrentFrameActionFrame)
            {
                Hero.SetJumpingVelocity();
            }
            if (ShouldTransitionOutOfJump())
            {
                Hero.SetState(new HeroFreeFallState(Hero));
            }
            Hero.ApplyJumpAirControl();
        }
        private bool ShouldTransitionOutOfJump()
        {
            return Hero.IsCurrentFrameActionFrame && ((!Hero.IsJumping && TimeSinceJumpActionFrameStart >= Hero.MinJumpTime) || (TimeSinceJumpActionFrameStart >= Hero.MaxJumpTime));
        }
    }
}