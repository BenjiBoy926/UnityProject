using UnityEngine;

namespace Abstract
{
    public class HeroDashState : HeroState
    {
        public HeroDashState(Hero hero) : base(hero) { }

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

        private void SetDashAnimation()
        {
            if (Mathf.Abs(Hero.DashAimX) > Mathf.Abs(Hero.DashAimY))
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