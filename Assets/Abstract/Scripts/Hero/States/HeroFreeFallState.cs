using Unity.VisualScripting.YamlDotNet.Serialization;

namespace Abstract
{
    public class HeroFreeFallState : HeroState
    {
        public HeroFreeFallState(Hero hero) : base(hero) { }

        public override void Enter()
        {
            base.Enter();
            Hero.HorizontalDirectionChanged += OnHeroHorizontalDirectionChanged;
            Hero.FinishedBackflipAnimation += OnHeroFinishedBackflipAnimation;
            if (!Hero.IsAnimatingBackflip)
            {
                SetFreeFallAnimation();
            }
        }
        public override void Exit()
        {
            base.Exit();
            Hero.HorizontalDirectionChanged -= OnHeroHorizontalDirectionChanged;
            Hero.FinishedBackflipAnimation -= OnHeroFinishedBackflipAnimation;
        }

        private void OnHeroHorizontalDirectionChanged()
        {
            if (!Hero.IsAnimatingBackflip)
            {
                SetFreeFallAnimation();
            }
        }
        private void OnHeroFinishedBackflipAnimation()
        {
            SetFreeFallAnimation();
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

        private void SetFreeFallAnimation()
        {
            if (Hero.HorizontalDirection == 0)
            {
                Hero.SetFreeFallStraightAnimation();
            }
            else if (Hero.HorizontalDirection != Hero.SpriteDirection)
            {
                Hero.SetFreeFallBackwardAnimation();
            }
            else
            {
                Hero.SetFreeFallForwardAnimation();
            }
        }
    }
}