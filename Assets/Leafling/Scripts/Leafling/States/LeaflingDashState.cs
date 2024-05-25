using UnityEngine;

namespace Leafling
{
    public class LeaflingDashState : LeaflingState
    {
        private Vector2 _aim;
        private bool _hasReachedActionFrame;
        private bool _dashOnRicochet;

        public LeaflingDashState(Leafling leafling, Vector2 aim, bool dashOnRicochet) : base(leafling) 
        {
            _aim = aim;
            _dashOnRicochet = dashOnRicochet;
        }

        public override void Enter()
        {
            base.Enter();
            Leafling.DashAnimationFinished += OnDashAnimationFinished;
            PrepareToDash();
        }
        public override void Exit()
        {
            base.Exit();
            Leafling.DashAnimationFinished -= OnDashAnimationFinished;
        }
        private void OnDashAnimationFinished()
        {
            Leafling.SetState(new LeaflingFreeFallState(Leafling));
        }
        private void PrepareToDash()
        {
            SetSquatAnimation();
            if (Leafling.IsTouchingAnything())
            {
                Leafling.ResetSpriteRotation();
            }
            else
            {
                LeaflingDashTools.SetRotation(Leafling, _aim);
            }
            Leafling.TransitionToDashAnimation(1, Leafling.DirectionToFlipX(_aim.x));
        }
        private void SetSquatAnimation()
        {
            if (Leafling.IsTouching(CardinalDirection.Down))
            {
                Leafling.SetSquatAnimation();
                Leafling.FaceTowards(_aim.x);
            }
            else if (Leafling.IsTouching(CardinalDirection.Up))
            {
                Leafling.SetCeilingPerchAnimation();
                Leafling.FaceTowards(_aim.x);
            }
            else if (Leafling.IsTouching(CardinalDirection.Right))
            {
                Leafling.SetWallPerchAnimation();
                Leafling.FaceTowards(-1);
            }
            else if (Leafling.IsTouching(CardinalDirection.Left))
            {
                Leafling.SetWallPerchAnimation();
                Leafling.FaceTowards(1);
            }
            else
            {
                Leafling.SetMidairDashAimAnimation();
                Leafling.FaceTowards(_aim.x);
            }
        }

        public override void Update(float dt)
        {
            base.Update(dt);
            if (Leafling.IsCurrentFrameActionFrame)
            {
                _hasReachedActionFrame = true;
            }
            Vector2 velocity = GetDashVelocity();
            Leafling.SetVelocity(velocity);
            CheckForRicochet();
        }
        private Vector2 GetDashVelocity()
        {
            if (!_hasReachedActionFrame)
            {
                return Vector2.zero;
            }
            else if (Leafling.IsCurrentFrameActionFrame)
            {
                return Leafling.MaxDashSpeed * _aim;
            }
            return Leafling.MaxDashSpeed * Leafling.EvaluateDashSpeedCurve(Leafling.ProgressAfterFirstActionFrame) * _aim;
        }

        private void CheckForRicochet()
        {
            if (!CanRicochet())
            {
                return;
            }
            if (GetRicochetNormal(out Vector2 normal))
            {
                Ricochet(normal);
            }
        }
        private bool CanRicochet()
        {
            return _hasReachedActionFrame && Leafling.IsTouchingAnything();
        }
        private bool GetRicochetNormal(out Vector2 normal)
        {
            normal = Vector2.zero;
            foreach (Vector2 contactNormal in Leafling.GetContactNormals())
            {
                if (CanRicochetOffOfNormal(contactNormal))
                {
                    normal = contactNormal;
                    return true;
                }
            }
            return false;
        }
        private bool CanRicochetOffOfNormal(Vector2 normal)
        {
            return Vector2.Dot(_aim, normal) < -0.01f;
        }
        private void Ricochet(Vector2 normal)
        {
            Vector2 ricochetDirection = GetRicochetAim(normal);
            if (_dashOnRicochet)
            {
                Leafling.SetState(new LeaflingDashState(Leafling, ricochetDirection, false));
            }
            else
            {
                Leafling.SetVelocity(ricochetDirection * Leafling.MaxDashSpeed);
                Leafling.FaceTowards(ricochetDirection.x);
                Leafling.SetState(new LeaflingFreeFallState(Leafling));
            }
        }
        private Vector2 GetRicochetAim(Vector2 normal)
        {
            return Vector2.Reflect(_aim, normal);
        }
    }
}