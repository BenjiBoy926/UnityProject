using UnityEngine;

namespace AbstractHumanoidToy
{
    public class SpriteBody : MonoBehaviour
    {
        [SerializeField]
        private SpriteBodyPart[] _parts;

        private void Reset()
        {
            _parts = GetComponentsInChildren<SpriteBodyPart>();
        }

        public void ShowFrame(SpriteAnimationFrame frame)
        {

        }
    }
}