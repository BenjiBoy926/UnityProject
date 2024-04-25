using UnityEngine;

namespace Abstract
{
    public class HeroDashState : HeroState
    {
        private Vector2 _aim;
        private bool _hasReachedActionFrame;

        public HeroDashState(Hero hero, Vector2 aim) : base(hero) 
        {
            _aim = aim;
        }

        public override void Enter()
        {
            base.Enter();
            Hero.DashAnimationFinished += OnDashAnimationFinished;
            SetDashAnimation();
        }
        public override void Exit()
        {
            base.Exit();
            Hero.DashAnimationFinished -= OnDashAnimationFinished;
        }
        private void OnDashAnimationFinished()
        {
            Hero.SetState(new HeroFreeFallState(Hero));
        }

        public override void Update(float dt)
        {
            base.Update(dt);
            if (Hero.IsCurrentFrameActionFrame)
            {
                _hasReachedActionFrame = true;
            }
            Vector2 velocity = GetDashVelocity();
            Hero.SetVelocity(velocity);
        }
        private Vector2 GetDashVelocity()
        {
            if (!_hasReachedActionFrame)
            {
                return Vector2.zero;
            }
            else if (Hero.IsCurrentFrameActionFrame)
            {
                return Hero.MaxDashSpeed * _aim;
            }
            return Hero.MaxDashSpeed * Hero.EvaluateDashSpeedCurve(Hero.ProgressAfterFirstActionFrame) * _aim;
        }

        private void SetDashAnimation()
        {
            if (Mathf.Abs(_aim.x) > Mathf.Abs(_aim.y))
            {
                Hero.SetSideDashAnimation();
            }
            else
            {
                Hero.SetUpDashAnimation();
            }
        }
    }
}