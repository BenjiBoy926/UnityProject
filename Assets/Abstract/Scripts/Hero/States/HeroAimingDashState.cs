using UnityEngine;

namespace Abstract
{
    public class HeroAimingDashState : HeroState
    {
        public HeroAimingDashState(Hero hero) : base(hero) { }

        public override void Enter()
        {
            base.Enter();
            Hero.SetVerticalVelocity(0);
            Hero.SetAimingDashGravityScale();
        }
        public override void Exit()
        {
            base.Exit();
            Hero.ResetGravityScale();
        }
        public override void Update(float dt)
        {
            base.Update(dt);
            ShowDashAim();
            if (!Hero.IsAimingDash)
            {
                Hero.SetState(new HeroFreeFallState(Hero));
            }
        }

        private void ShowDashAim()
        {
            if (Hero.IsOnGround)
            {
                Hero.TransitionToSquatAnimation(0.5f);
            }
            else
            {
                SetAimingDashInAirAnimation();
            }
            Hero.SetFacingDirection(Hero.DashAimX);
        }
        private void SetAimingDashInAirAnimation()
        {
            if (Mathf.Abs(Hero.DashAimX) > Mathf.Abs(Hero.DashAimY))
            {
                Hero.TransitionToSideDashPrepAnimation(0.5f);
            }
            else
            {
                Hero.TransitionToSquatAnimation(0.5f);
            }
        }
    }
}