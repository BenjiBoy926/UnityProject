using UnityEngine;
using UnityEngine.UIElements;

namespace Abstract
{
    public class HeroAimingDashState : HeroState
    {
        private Vector2 Aim
        {
            get
            {
                if (Hero.IsTouchingGround && Hero.DashAimY < 0)
                {
                    return new Vector2(Hero.DashAimX, 0).normalized;
                }
                return Hero.DashAim;
            }
        }

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
                Hero.SetState(new HeroDashState(Hero, Aim));
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
                Hero.TransitionToSquatAnimation(0.5f, CurrentFlip());
            }
            else
            {
                Hero.TransitionToMidairDashAimAnimation(0.5f, CurrentFlip());
            }
        }
        private SpriteAnimationFlip CurrentFlip()
        {
            if (Hero.IsTouchingGround)
            {
                return new SpriteAnimationFlip(Aim.x < 0, false);
            }
            else
            {
                return new SpriteAnimationFlip(false, Aim.x < 0);
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
            Hero.SetSpriteRight(Aim);
        }
    }
}