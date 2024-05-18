using UnityEngine;

namespace Abstract
{
    public class HeroOnGroundState : HeroState
    {
        private readonly FromToCurve _toRun;
        private readonly FromToCurve _toLeap;

        public HeroOnGroundState(Hero hero) : base(hero) 
        {
            _toRun = new FromToCurve(0, hero.BaseRunSpeed, hero.RunAccelerationCurve);
            _toLeap = new FromToCurve(hero.BaseRunSpeed, hero.LeapMaxSpeed, hero.RunAccelerationCurve);
        }

        public override void Enter()
        {
            base.Enter();
            Hero.HorizontalDirectionChanged += OnHeroDirectionChanged;
            ReflectCurrentDirection();
        }
        public override void Exit()
        {
            base.Exit();
            Hero.HorizontalDirectionChanged -= OnHeroDirectionChanged;
        }

        private void OnHeroDirectionChanged()
        {
            ReflectCurrentDirection();
        }

        public override void Update(float dt)
        {
            base.Update(dt);
            float speed = CalculateHorizontalSpeed();
            Hero.SetHorizontalVelocity(speed);
            if (!Hero.IsTouchingGround)
            {
                Hero.SetState(new HeroFreeFallState(Hero));
            }
            if (Hero.IsJumping)
            {
                Hero.SetState(new HeroJumpState(Hero));
            }
            if (Hero.IsAimingDash)
            {
                Hero.SetState(new HeroAimingDashState(Hero));
            }
        }
        private float CalculateHorizontalSpeed()
        {
            if (Hero.IsAnimatingIdle)
            {
                return 0;
            }
            if (Hero.IsCurrentFrameFirstFrame)
            {
                return Hero.FacingDirection * _toRun.Evaluate(Hero.CurrentFrameProgress);
            }
            if (Hero.IsTransitioningOnCurrentFrame)
            {
                return Hero.FacingDirection * _toRun.Evaluate(1 - Hero.CurrentFrameProgress);
            }
            if (Hero.IsNextFrameActionFrame)
            {
                return Hero.FacingDirection * _toLeap.Evaluate(Hero.CurrentFrameProgress);
            }
            if (Hero.IsPreviousFrameActionFrame)
            {
                return Hero.FacingDirection * _toLeap.Evaluate(1 - Hero.CurrentFrameProgress);
            }
            if (Hero.IsCurrentFrameActionFrame)
            {
                return Hero.FacingDirection * Hero.LeapMaxSpeed;
            }
            return Hero.FacingDirection * Hero.BaseRunSpeed;
        }

        private void ReflectCurrentDirection()
        {
            TransitionAnimation();
        }

        private void TransitionAnimation()
        {
            if (Hero.HorizontalDirection == 0)
            {
                Hero.TransitionToIdleAnimation(0.3f, Hero.SpriteFlipX);
            }
            else
            {
                Hero.TransitionToRunAnimation(0.3f, IntendedFlipX());
            }
        }
        private bool IntendedFlipX()
        {
            return Hero.DirectionToFlipX(Hero.HorizontalDirection);
        }
    }
}