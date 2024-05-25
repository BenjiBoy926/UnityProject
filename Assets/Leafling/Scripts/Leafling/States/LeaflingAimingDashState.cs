using UnityEngine;
using UnityEngine.UIElements;

namespace Abstract
{
    public class LeaflingAimingDashState : LeaflingState
    {
        private Vector2 Aim
        {
            get
            {
                if (Leafling.IsTouching(CardinalDirection.Down) && Leafling.DashAimY < 0)
                {
                    return new Vector2(Leafling.DashAimX, 0).normalized;
                }
                return Leafling.DashAim;
            }
        }

        public LeaflingAimingDashState(Leafling leafling) : base(leafling) { }

        public override void Enter()
        {
            base.Enter();
            Leafling.SetVerticalVelocity(0);
            Leafling.SetAimingDashGravityScale();
        }
        public override void Exit()
        {
            base.Exit();
            Leafling.ResetGravityScale();
            Leafling.ResetSpriteRotation();
        }
        public override void Update(float dt)
        {
            base.Update(dt);
            ShowDashAim();
            if (!Leafling.IsAimingDash)
            {
                Leafling.SetState(new LeaflingDashState(Leafling, Aim, true));
            }
        }

        private void ShowDashAim()
        {
            TransitionToAnimation();
            SetSpriteRotation();
        }
        private void TransitionToAnimation()
        {
            if (Leafling.IsTouching(CardinalDirection.Down))
            {
                Leafling.TransitionToSquatAnimation(0.5f, FlipXTowardsAim());
            }
            else if (Leafling.IsTouching(CardinalDirection.Up))
            {
                Leafling.TransitionToCeilingPerchAnimation(0.5f, FlipXTowardsAim());
            }
            else if (Leafling.IsTouching(CardinalDirection.Right))
            {
                Leafling.TransitionToWallPerchAnimation(0.5f, Leafling.DirectionToFlipX(-1));
            }
            else if (Leafling.IsTouching(CardinalDirection.Left))
            {
                Leafling.TransitionToWallPerchAnimation(0.5f, Leafling.DirectionToFlipX(1));
            }
            else
            {
                Leafling.TransitionToMidairDashAimAnimation(0.5f, FlipXTowardsAim());
            }
        }
        private bool FlipXTowardsAim()
        {
            return Leafling.DirectionToFlipX(Aim.x);
        }
        private void SetSpriteRotation()
        {
            if (Leafling.IsTouchingAnything())
            {
                Leafling.ResetSpriteRotation();
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
            Leafling.SetSpriteRight(spriteRight);
        }
    }
}