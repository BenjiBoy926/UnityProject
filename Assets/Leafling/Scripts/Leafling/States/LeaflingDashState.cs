using UnityEngine;

namespace Abstract
{
    public class LeaflingDashState : LeaflingState
    {
        private Vector2 _aim;
        private bool _hasReachedActionFrame;
        private bool _richochetEnabled;

        public LeaflingDashState(Leafling leafling, Vector2 aim, bool ricochetEnabled) : base(leafling) 
        {
            _aim = aim;
            _richochetEnabled = ricochetEnabled;
        }

        public override void Enter()
        {
            base.Enter();
            Leafling.DashAnimationFinished += OnDashAnimationFinished;
            SetDashAnimation();
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
        private void SetDashAnimation()
        {
            Leafling.FaceTowards(_aim.x);
            Leafling.SetSideDashAnimation();
            // NOTE: this logic is duplicated in aiming dash state, need to collapse it somehow
            Vector2 spriteRight = _aim;
            if (spriteRight.x < 0)
            {
                spriteRight *= -1;
            }
            Leafling.SetSpriteRight(spriteRight);
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
            return _richochetEnabled && _hasReachedActionFrame && Leafling.IsTouchingAnything();
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
            return Vector2.Dot(_aim, normal) < -0.2f;
        }
        private void Ricochet(Vector2 normal)
        {
            Leafling.SetState(new LeaflingDashState(Leafling, GetRicochetAim(normal), false));
        }
        private Vector2 GetRicochetAim(Vector2 normal)
        {
            return Vector2.Reflect(_aim, normal);
        }
    }
}