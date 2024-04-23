namespace Abstract
{
    public class HeroFreeFallState : HeroState
    {
        public HeroFreeFallState(Hero hero) : base(hero) { }

        public override void Enter()
        {
            base.Enter();
            Hero.HorizontalDirectionChanged += OnHeroHorizontalDirectionChanged;
            Hero.SetBackflipAnimation();
            TransitionFreeFallAnimation();
        }
        public override void Exit()
        {
            base.Exit();
            Hero.HorizontalDirectionChanged -= OnHeroHorizontalDirectionChanged;
        }

        private void OnHeroHorizontalDirectionChanged()
        {
            TransitionFreeFallAnimation();
        }

        public override void Update(float dt)
        {
            base.Update(dt);
            Hero.ApplyFreeFallAirControl();
            if (Hero.IsOnGround)
            {
                Hero.SetLandingAnimation();
                Hero.SetState(new HeroOnGroundState(Hero));
            }
        }

        private void TransitionFreeFallAnimation()
        {
            if (Hero.HorizontalDirection == 0)
            {
                Hero.TransitionToFreeFallStraightAnimation(1);
            }
            else if (Hero.HorizontalDirection != Hero.SpriteDirection)
            {
                Hero.TransitionToFreeFallBackwardAnimation(1);
            }
            else
            {
                Hero.TransitionToFreeFallForwardAnimation(1);
            }
        }
    }
}