namespace AbstractHumanoidToy
{
    public class HeroFreeFallState : HeroState
    {
        public HeroFreeFallState(Hero hero) : base(hero) { }

        public override void Enter()
        {
            base.Enter();
            Hero.HorizontalDirectionChanged += OnHeroHorizontalDirectionChanged;
            ReflectCurrentHorizontalDirection();
        }
        public override void Exit()
        {
            base.Exit();
            Hero.HorizontalDirectionChanged -= OnHeroHorizontalDirectionChanged;
        }

        private void OnHeroHorizontalDirectionChanged()
        {
            ReflectCurrentHorizontalDirection();
        }

        public override void Update(float dt)
        {
            base.Update(dt);
            Hero.ApplyHorizontalAirControlForce();
            Hero.ClampHorizontalAirSpeed();
        }

        private void ReflectCurrentHorizontalDirection()
        {
            if (Hero.HorizontalDirection == 0)
            {
                Hero.TransitionToFreeFallStraightAnimation();
            }
            else if (Hero.HorizontalDirection != Hero.SpriteDirection)
            {
                Hero.TransitionToFreeFallBackwardAnimation();
            }
            else
            {
                Hero.TransitionToFreeFallForwardAnimation();
            }
        }
    }
}