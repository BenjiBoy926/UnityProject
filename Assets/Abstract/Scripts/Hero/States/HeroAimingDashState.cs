using UnityEngine;
using UnityEngine.UIElements;

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
            Hero.ResetSpriteRotation();
        }
        public override void Update(float dt)
        {
            base.Update(dt);
            ShowDashAim();
            if (!Hero.IsAimingDash)
            {
                Hero.SetState(new HeroDashState(Hero));
            }
        }

        private void ShowDashAim()
        {
            TransitionToAnimation();
            SetSpriteRotation();
            Hero.SetFacingDirection(Hero.DashAimX);
        }
        private void TransitionToAnimation()
        {
            if (Hero.IsTouchingGround)
            {
                Hero.TransitionToSquatAnimation(0.5f);
            }
            else
            {
                Hero.TransitionToSideDashPrepAnimation(0.5f);
            }
        }
        private void SetSpriteRotation()
        {
            if (Hero.IsTouchingGround)
            {
                Hero.ResetSpriteRotation();
            }
            else
            {
                SetSpriteMidairRotation();
            }
        }
        private void SetSpriteMidairRotation()
        {
            Vector2 aim = Hero.DashAim;
            if (aim.x < 0)
            {
                aim *= -1;
            }
            Hero.SetSpriteRight(aim);
        }
    }
}