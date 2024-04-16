using UnityEngine;

namespace AbstractHumanoidToy
{
    public class SpriteBodyPart : MonoBehaviour
    {
        [SerializeField]
        private SpriteBodyPartID _id;
        [SerializeField]
        private SpriteRenderer _renderer;

        private void Reset()
        {
            _renderer = GetComponent<SpriteRenderer>();
        }
    }
}