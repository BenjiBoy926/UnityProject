using UnityEngine;

namespace Abstract
{
    public class HeroDashState : HeroState
    {
        private Vector2 _aim;
        private bool _hasReachedActionFrame;
        private bool _richochetEnabled;

        public HeroDashState(Hero hero, Vector2 aim, bool ricochetEnabled) : base(hero) 
        {
            _aim = aim;
            _richochetEnabled = ricochetEnabled;
        }

        public override void Enter()
        {
            base.Enter();
            Hero.DashAnimationFinished += OnDashAnimationFinished;
            SetDashAnimation();
        }
        public override void Exit()
        {
            base.Exit();
            Hero.DashAnimationFinished -= OnDashAnimationFinished;
        }
        private void OnDashAnimationFinished()
        {
            Hero.SetState(new HeroFreeFallState(Hero));
        }
        private void SetDashAnimation()
        {
            Hero.FaceTowards(_aim.x);
            Hero.SetSideDashAnimation();
            // NOTE: this logic is duplicated in aiming dash state, need to collapse it somehow
            Vector2 spriteRight = _aim;
            if (spriteRight.x < 0)
            {
                spriteRight *= -1;
            }
            Hero.SetSpriteRight(spriteRight);
        }

        public override void Update(float dt)
        {
            base.Update(dt);
            if (Hero.IsCurrentFrameActionFrame)
            {
                _hasReachedActionFrame = true;
            }
            Vector2 velocity = GetDashVelocity();
            Hero.SetVelocity(velocity);
            CheckForRicochet();
        }
        private Vector2 GetDashVelocity()
        {
            if (!_hasReachedActionFrame)
            {
                return Vector2.zero;
            }
            else if (Hero.IsCurrentFrameActionFrame)
            {
                return Hero.MaxDashSpeed * _aim;
            }
            return Hero.MaxDashSpeed * Hero.EvaluateDashSpeedCurve(Hero.ProgressAfterFirstActionFrame) * _aim;
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
            return _richochetEnabled && _hasReachedActionFrame && Hero.IsTouchingAnything();
        }
        private bool GetRicochetNormal(out Vector2 normal)
        {
            normal = Vector2.zero;
            foreach (Vector2 contactNormal in Hero.GetContactNormals())
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
            Hero.SetState(new HeroDashState(Hero, GetRicochetAim(normal), false));
        }
        private Vector2 GetRicochetAim(Vector2 normal)
        {
            return Vector2.Reflect(_aim, normal);
        }
    }
}