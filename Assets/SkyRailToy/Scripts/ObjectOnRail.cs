using System.Collections;
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
            GoToCurrentRail();
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
            _rigidbody.position = GetTargetPosition();
        }
        private void AnimateToCurrentRail()
        {

        }

        private Vector2 GetTargetPosition()
        {
            float targetFeetHeight = _railing.GetRailAltitude(_currentRailIndex);
            float feetToBody = _rigidbody.position.y - FeetHeight;
            return new Vector2(_rigidbody.position.x, targetFeetHeight + feetToBody);
        }
    }
}