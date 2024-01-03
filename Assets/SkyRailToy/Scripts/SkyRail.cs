using System.Collections;
using UnityEngine;

namespace SkyRailToy
{
    public class SkyRail : MonoBehaviour
    {
        public float Altitude => _renderer.bounds.max.y;

        [SerializeField]
        private SpriteRenderer _renderer;

        private void Reset()
        {
            _renderer = GetComponent<SpriteRenderer>();
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