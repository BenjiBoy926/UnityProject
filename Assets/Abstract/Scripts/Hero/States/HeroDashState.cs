using UnityEngine;

namespace Abstract
{
    public class HeroDashState : HeroState
    {
        private Vector2 _aim;
        private bool _hasReachedActionFrame;

        public HeroDashState(Hero hero, Vector2 aim) : base(hero) 
        {
            _aim = aim;
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
            Hero.SetFacingDirection(_aim.x);
            if (Mathf.Abs(_aim.x) > Mathf.Abs(_aim.y))
            {
                Hero.SetSideDashAnimation();
            }
            else
            {
                Hero.SetUpDashAnimation();
            }
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
            return _hasReachedActionFrame && Hero.GetContactCount() > 0;
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
            Hero.SetState(new HeroDashState(Hero, GetRicochetAim(normal)));
        }
        private Vector2 GetRicochetAim(Vector2 normal)
        {
            return Vector2.Reflect(_aim, normal);
        }
    }
}