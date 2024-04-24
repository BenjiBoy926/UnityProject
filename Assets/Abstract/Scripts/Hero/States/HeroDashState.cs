using UnityEngine;

namespace Abstract
{
    public class HeroDashState : HeroState
    {
        private Vector2 _aim;

        public HeroDashState(Hero hero) : base(hero) { }

        public override void Enter()
        {
            base.Enter();
            _aim = Hero.DashAim;
            Hero.DashAnimationFinished += OnDashAnimationFinished;
            SetDashAnimation();
        }
        public override void Exit()
        {
            base.Exit();
            Hero.DashAnimationFinished -= OnDashAnimationFinished;
        }
        public override void Update(float dt)
        {
            base.Update(dt);
            Hero.SetVelocity(_aim * Hero.MaxDashSpeed);
        }

        private void OnDashAnimationFinished()
        {
            Hero.SetState(new HeroFreeFallState(Hero));
        }

        private void SetDashAnimation()
        {
            if (_aim.x > _aim.y)
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