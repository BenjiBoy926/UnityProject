using UnityEngine;

namespace Abstract
{
    public class HeroOnGroundState : HeroState
    {
        private FromToCurve _toRun;
        private FromToCurve _toLeap;

        public HeroOnGroundState(Hero hero) : base(hero) 
        {
            _toRun = new FromToCurve(0, hero.BaseRunSpeed, hero.RunAccelerationCurve);
            _toLeap = new FromToCurve(hero.BaseRunSpeed, hero.LeapMaxSpeed, hero.RunAccelerationCurve);
        }

        public override void Enter()
        {
            base.Enter();
            Hero.HorizontalDirectionChanged += OnHeroDirectionChanged;
            Hero.StartedJumping += OnHeroStartedJumping;
            ReflectCurrentDirection();
        }
        public override void Exit()
        {
            base.Exit();
            Hero.HorizontalDirectionChanged -= OnHeroDirectionChanged;
            Hero.StartedJumping -= OnHeroStartedJumping;
        }

        private void OnHeroDirectionChanged()
        {
            ReflectCurrentDirection();
        }
        private void OnHeroStartedJumping()
        {
            Hero.SetState(new HeroJumpState(Hero));
        }

        public override void Update(float dt)
        {
            base.Update(dt);
            if (!Hero.IsOnGround)
            {
                Hero.SetState(new HeroFreeFallState(Hero));
            }
            float speed = CalculateHorizontalSpeed();
            Hero.SetHorizontalVelocity(speed);
        }
        private float CalculateHorizontalSpeed()
        {
            if (Hero.IsAnimatingIdle)
            {
                return 0;
            }
            if (Hero.IsCurrentFrameFirstFrame)
            {
                return Hero.SpriteDirection * _toRun.Evaluate(Hero.CurrentFrameProgress);
            }
            if (Hero.IsTransitioningOnCurrentFrame)
            {
                return Hero.SpriteDirection * _toRun.Evaluate(1 - Hero.CurrentFrameProgress);
            }
            if (Hero.IsNextFrameActionFrame)
            {
                return Hero.SpriteDirection * _toLeap.Evaluate(Hero.CurrentFrameProgress);
            }
            if (Hero.IsPreviousFrameActionFrame)
            {
                return Hero.SpriteDirection * _toLeap.Evaluate(1 - Hero.CurrentFrameProgress);
            }
            if (Hero.IsCurrentFrameActionFrame)
            {
                return Hero.SpriteDirection * Hero.LeapMaxSpeed;
            }
            return Hero.SpriteDirection * Hero.BaseRunSpeed;
        }

        private void ReflectCurrentDirection()
        {
            TransitionAnimation();
            TransitionFlipX();
        }

        private void TransitionAnimation()
        {
            if (Hero.HorizontalDirection == 0)
            {
                Hero.TransitionToIdleAnimation(0.3f);
            }
            else
            {
                Hero.TransitionToRunAnimation(0.3f);
            }
        }
        private void TransitionFlipX()
        {
            if (Hero.HorizontalDirection != 0)
            {
                Hero.TransitionFlipX(DirectionToFlipX(Hero.HorizontalDirection));
            }
        }

        private static bool DirectionToFlipX(int direction)
        {
            return direction == -1;
        }
    }
}