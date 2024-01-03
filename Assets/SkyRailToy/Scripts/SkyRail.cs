using System.Collections;
using UnityEngine;

namespace SkyRailToy
{
    public class SkyRail : MonoBehaviour
    {
        [SerializeField]
        private Collider2D[] _colliders;

        private void Reset()
        {
            _colliders = GetComponentsInChildren<Collider2D>(true);
        }

        public void Catch()
        {
            SetCollidersEnabled(true);
        }
        public void Release()
        {
            SetCollidersEnabled(false);
        }

        private void SetCollidersEnabled(bool enabled)
        {
            foreach (var collider in _colliders)
            {
                collider.enabled = enabled;
            }
        }
    }
}