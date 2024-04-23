namespace Abstract
{
    public class HeroAimingDashState : HeroState
    {
        public HeroAimingDashState(Hero hero) : base(hero) { }

        public override void Enter()
        {
            base.Enter();
            Hero.IsAimingDashChanged += OnIsAimingDashChanged;
            Hero.SetVerticalVelocity(0);
        }

        private void OnIsAimingDashChanged()
        {
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