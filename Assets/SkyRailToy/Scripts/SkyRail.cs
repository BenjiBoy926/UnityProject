using System.Collections;
using UnityEngine;

namespace SkyRailToy
{
    public class SkyRail : MonoBehaviour
    {
        public float Altitude => _collider.bounds.max.y;

        [SerializeField]
        private SpriteRenderer _renderer;
        [SerializeField]
        private Collider2D _collider;

        private void Reset()
        {
            _renderer = GetComponent<SpriteRenderer>();
            _collider = GetComponent<Collider2D>();
        }

        public void SetColor(Color color)
        {
            _renderer.color = color;
        }
        public void SetLocalPosition(Vector3 position)
        {
            transform.localPosition = position;
        }
    }
}