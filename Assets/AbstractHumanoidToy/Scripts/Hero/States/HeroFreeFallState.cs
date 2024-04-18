namespace AbstractHumanoidToy
{
    public class HeroFreeFallState : HeroState
    {
        public HeroFreeFallState(Hero hero) : base(hero) { }

        public override void Update(float dt)
        {
            base.Update(dt);
            Hero.ApplyHorizontalAirControlForce();
            Hero.ClampHorizontalAirSpeed();
        }
    }
}