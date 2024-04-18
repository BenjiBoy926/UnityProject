using UnityEngine;

namespace AbstractHumanoidToy
{
    public class HeroOnGroundState : HeroState
    {
        private FromToCurve _toRun;
        private FromToCurve _toLeap;
        private bool _isTransitioningToJump;

        public HeroOnGroundState(Hero hero) : base(hero) 
        {
            _toRun = new FromToCurve(0, hero.BaseRunSpeed, hero.RunAccelerationCurve);
            _toLeap = new FromToCurve(hero.BaseRunSpeed, hero.LeapMaxSpeed, hero.RunAccelerationCurve);
        }

        public override void Enter(float time)
        {
            base.Enter(time);
            Hero.HorizontalDirectionChanged += OnHeroDirectionChanged;
            Hero.StartedJumping += OnHeroStartedJumping;
            Hero.StartedJumpAnimation += OnHeroStartedJumpAnimation;
            ReflectCurrentDirection();
        }
        public override void Exit()
        {
            base.Exit();
            Hero.HorizontalDirectionChanged -= OnHeroDirectionChanged;
            Hero.StartedJumping -= OnHeroStartedJumping;
            Hero.StartedJumpAnimation -= OnHeroStartedJumpAnimation;
        }

        private void OnHeroDirectionChanged()
        {
            if (_isTransitioningToJump)
            {
                return;
            }
            ReflectCurrentDirection();
        }
        private void OnHeroStartedJumping()
        {
            _isTransitioningToJump = true;
            Hero.TransitionToJumpAnimation();
        }
        private void OnHeroStartedJumpAnimation()
        {
            Hero.SetState(new HeroJumpState(Hero));
        }

        public override void Update(float dt)
        {
            base.Update(dt);
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
                Hero.TransitionToIdleAnimation();
            }
            else
            {
                Hero.TransitionToRunAnimation();
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