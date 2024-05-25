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
                if (Hero.IsTouching(CardinalDirection.Down) && Hero.DashAimY < 0)
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
                Hero.SetState(new HeroDashState(Hero, Aim, true));
            }
        }

        private void ShowDashAim()
        {
            TransitionToAnimation();
            SetSpriteRotation();
            Hero.FaceTowards(Hero.DashAimX);
        }
        private void TransitionToAnimation()
        {
            if (Hero.IsTouching(CardinalDirection.Down))
            {
                Hero.TransitionToSquatAnimation(0.5f, FlipXTowardsAim());
            }
            else if (Hero.IsTouching(CardinalDirection.Up))
            {
                Hero.TransitionToCeilingPerchAnimation(0.5f, FlipXTowardsAim());
            }
            else if (Hero.IsTouching(CardinalDirection.Right))
            {
                Hero.TransitionToWallPerchAnimation(0.5f, Hero.DirectionToFlipX(-1));
            }
            else if (Hero.IsTouching(CardinalDirection.Left))
            {
                Hero.TransitionToWallPerchAnimation(0.5f, Hero.DirectionToFlipX(1));
            }
            else
            {
                Hero.TransitionToMidairDashAimAnimation(0.5f, FlipXTowardsAim());
            }
        }
        private bool FlipXTowardsAim()
        {
            return Hero.DirectionToFlipX(Aim.x);
        }
        private void SetSpriteRotation()
        {
            if (Hero.IsTouchingAnything())
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
            Vector2 spriteRight = Aim;
            if (spriteRight.x < 0)
            {
                spriteRight *= -1;
            }
            Hero.SetSpriteRight(spriteRight);
        }
    }
}