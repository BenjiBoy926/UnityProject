namespace Abstract
{
    public class LeaflingFreeFallState : LeaflingState
    {
        public LeaflingFreeFallState(Leafling leafling) : base(leafling) { }

        public override void Enter()
        {
            base.Enter();
            Leafling.HorizontalDirectionChanged += OnLeaflingHorizontalDirectionChanged;
            Leafling.ResetSpriteRotation();
            Leafling.SetBackflipAnimation();
            TransitionFreeFallAnimation();
        }
        public override void Exit()
        {
            base.Exit();
            Leafling.HorizontalDirectionChanged -= OnLeaflingHorizontalDirectionChanged;
        }

        private void OnLeaflingHorizontalDirectionChanged()
        {
            TransitionFreeFallAnimation();
        }

        public override void Update(float dt)
        {
            base.Update(dt);
            Leafling.ApplyFreeFallAirControl();
            if (Leafling.IsTouching(CardinalDirection.Down))
            {
                Leafling.SetSquatAnimation();
                Leafling.SetState(new LeaflingOnGroundState(Leafling));
            }
            if (Leafling.IsAimingDash)
            {
                Leafling.SetState(new LeaflingAimingDashState(Leafling));
            }
        }

        private void TransitionFreeFallAnimation()
        {
            if (Leafling.HorizontalDirection == 0)
            {
                Leafling.TransitionToFreeFallStraightAnimation(1, Leafling.SpriteFlipX);
            }
            else if (Leafling.HorizontalDirection != Leafling.FacingDirection)
            {
                Leafling.TransitionToFreeFallBackwardAnimation(1, Leafling.SpriteFlipX);
            }
            else
            {
                Leafling.TransitionToFreeFallForwardAnimation(1, Leafling.SpriteFlipX);
            }
        }
    }
}