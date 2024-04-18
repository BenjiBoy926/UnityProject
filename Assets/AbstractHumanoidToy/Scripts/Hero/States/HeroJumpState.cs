using UnityEngine;

namespace AbstractHumanoidToy
{
    public class HeroJumpState : HeroState
    {
        private float TimeSinceJumpActionFrameStart => Time.time - _timeOfJumpActionFrameStart;

        private bool _isJumping = true;
        private float _timeOfJumpActionFrameStart;

        public HeroJumpState(Hero hero) : base(hero) { }

        public override void Enter()
        {
            base.Enter();
            Hero.StoppedJumping += OnHeroStoppedJumping;
            Hero.ActionFrameEntered += OnActionFrameEntered;
        }
        public override void Exit()
        {
            base.Exit();
            Hero.StoppedJumping -= OnHeroStoppedJumping;
            Hero.ActionFrameEntered -= OnActionFrameEntered;
        }

        private void OnActionFrameEntered()
        {
            _timeOfJumpActionFrameStart = Time.time;
        }
        private void OnHeroStoppedJumping()
        {
            _isJumping = false;
        }

        public override void Update(float dt)
        {
            base.Update(dt);
            if (Hero.IsCurrentFrameActionFrame)
            {
                Hero.SetJumpingVelocity();
            }
            if (ShouldTransitionOutOfJump())
            {
                Hero.SetState(new HeroOnGroundState(Hero));
            }
        }
        private bool ShouldTransitionOutOfJump()
        {
            return Hero.IsCurrentFrameActionFrame && ((!_isJumping && TimeSinceJumpActionFrameStart >= Hero.MinJumpTime) || (TimeSinceJumpActionFrameStart >= Hero.MaxJumpTime));
        }
    }
}