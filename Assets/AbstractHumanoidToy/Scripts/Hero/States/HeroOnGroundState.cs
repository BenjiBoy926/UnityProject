using UnityEngine;

namespace AbstractHumanoidToy
{
    public class HeroOnGroundState : HeroState
    {
        public HeroOnGroundState(Hero hero) : base(hero) { }

        public override void Enter(float time)
        {
            base.Enter(time);
            Hero.CurrentDirectionChanged += OnHeroDirectionChanged;
            ReflectCurrentDirection();
        }
        public override void Exit()
        {
            base.Exit();
            Hero.CurrentDirectionChanged -= OnHeroDirectionChanged;
        }

        private void OnHeroDirectionChanged()
        {
            ReflectCurrentDirection();
        }

        public override void Update(float dt)
        {
            base.Update(dt);
        }

        private void ReflectCurrentDirection()
        {
            TransitionAnimation();
            TransitionFlipX();
        }

        private void TransitionAnimation()
        {
            if (Hero.CurrentDirection == 0)
            {
                Hero.TransitionToIdle();
            }
            else
            {
                Hero.TransitionToRun();
            }
        }
        private void TransitionFlipX()
        {
            if (Hero.CurrentDirection != 0)
            {
                Hero.TransitionFlipX(DirectionToFlipX(Hero.CurrentDirection));
            }
        }

        private static bool DirectionToFlipX(int direction)
        {
            return direction == -1;
        }
        private static int FlipXToDirection(bool flipX)
        {
            return flipX ? -1 : 1;
        }
    }
}