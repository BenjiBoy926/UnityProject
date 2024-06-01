namespace Leafling
{
    public class LeaflingFreeFallState : LeaflingState
    {
        private FreeFallEntry _entry;

        public LeaflingFreeFallState(Leafling leafling, FreeFallEntry entry) : base(leafling)
        {
            _entry = entry;
        }

        public override void Enter()
        {
            base.Enter();
            Leafling.HorizontalDirectionChanged += OnLeaflingHorizontalDirectionChanged;
            Leafling.ResetSpriteRotation();
            if (_entry == FreeFallEntry.Backflip)
            {
                Leafling.SetBackflipAnimation();
            }
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
            if (Leafling.IsJumping)
            {
                Leafling.SetState(new LeaflingFlutterState(Leafling));
            }
        }

        private void TransitionFreeFallAnimation()
        {
            if (Leafling.HorizontalDirection == 0)
            {
                Leafling.TransitionToFreeFallStraightAnimation(1, Leafling.CurrentFlipX);
            }
            else if (Leafling.HorizontalDirection != Leafling.FacingDirection)
            {
                Leafling.TransitionToFreeFallBackwardAnimation(1, Leafling.CurrentFlipX);
            }
            else
            {
                Leafling.TransitionToFreeFallForwardAnimation(1, Leafling.CurrentFlipX);
            }
        }
    }
}