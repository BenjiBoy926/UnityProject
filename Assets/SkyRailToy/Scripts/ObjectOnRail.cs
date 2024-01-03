using DG.Tweening;
using UnityEngine;

namespace SkyRailToy
{
    public class ObjectOnRail : MonoBehaviour
    {
        private float FeetHeight => _collider.bounds.min.y;

        [SerializeField]
        private Rigidbody2D _rigidbody;
        [SerializeField]
        private Collider2D _collider;
        [SerializeField]
        private int _currentRailIndex = 0;

        [SerializeField]
        private float _animationDuration = 0.3f;
        [SerializeField]
        private Ease _moveUpEase = Ease.OutBack;
        [SerializeField]
        private Ease _moveDownEase = Ease.OutBounce;

        private SkyRailing _railing;

        private void Reset()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _collider = GetComponentInChildren<Collider2D>(true);
        }
        private void OnValidate()
        {
            if (_rigidbody == null || _collider == null || _railing == null)
            {
                return;
            }
            AnimateToCurrentRail();
        }
        private void Awake()
        {
            _railing = FindFirstObjectByType<SkyRailing>();
        }
        private void Start()
        {
            GoToCurrentRail();
        }

        public void MoveUp()
        {
            SetCurrentRail(_currentRailIndex + 1);
        }
        public void MoveDown()
        {
            SetCurrentRail(_currentRailIndex - 1);
        }
        private void SetCurrentRail(int currentRail)
        {
            currentRail = Mathf.Clamp(currentRail, 0, _railing.RailCount);
            if (currentRail == _currentRailIndex)
            {
                return;
            }
            _currentRailIndex = currentRail;
            AnimateToCurrentRail();
        }

        private void GoToCurrentRail()
        {
            Vector2 targetPosition = new Vector2(_rigidbody.position.x, GetTargetAltitude());
            _rigidbody.position = targetPosition;
        }
        private void AnimateToCurrentRail()
        {
            _rigidbody.DOKill();
            
            float targetAltitude = GetTargetAltitude();
            Ease ease = GetAnimationEase();
            _rigidbody.DOMoveY(targetAltitude, _animationDuration)
                .SetEase(ease);
        }
        private Ease GetAnimationEase()
        {
            if (ShouldMoveDown())
            {
                return _moveDownEase;
            }
            return _moveUpEase;
        }
        private bool ShouldMoveDown()
        {
            return GetTargetAltitude() < _rigidbody.position.y;
        }

        private float GetTargetAltitude()
        {
            float targetFeetHeight = _railing.GetRailAltitude(_currentRailIndex);
            float feetToBody = _rigidbody.position.y - FeetHeight;
            return targetFeetHeight + feetToBody;
        }
    }
}