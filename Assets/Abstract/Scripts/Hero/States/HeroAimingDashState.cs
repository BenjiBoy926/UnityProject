namespace Abstract
{
    public class HeroAimingDashState : HeroState
    {
        public HeroAimingDashState(Hero hero) : base(hero) { }

        public override void Enter()
        {
            base.Enter();
            Hero.SetVerticalVelocity(0);
        }
        public override void Update(float dt)
        {
            base.Update(dt);
            if (!Hero.IsAimingDash)
            {
                Hero.SetState(new HeroFreeFallState(Hero));
            }
        }

        private void ShowDashAim()
        {

        }
    }
}