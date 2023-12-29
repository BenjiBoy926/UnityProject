using System.Collections;
using UnityEngine;

namespace GunToy
{
    public class MuzzleFlash : MonoBehaviour
    {
        private float TimeSinceEffectStart => Time.time - _effectStartTime;

        [SerializeField]
        private LineRenderer _renderer;
        [SerializeField]
        private float _duration = 0.05f;
        private float _effectStartTime = 0;

        public void Play()
        {
            _effectStartTime = Time.time;
        }

        private void Update()
        {
            _renderer.enabled = TimeSinceEffectStart < _duration;
        }
    }
}