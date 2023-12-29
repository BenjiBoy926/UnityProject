using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GravityToy
{
    public class GravityWell : MonoBehaviour
    {
        public float Strength => _strength;
        public Vector3 Position => transform.position;

        [SerializeField]
        private float _strength = 10;
        [SerializeField]
        private List<GravityReceiver> _receivers;

        public void SetIsEnabled(bool isEnabled)
        {
            enabled = isEnabled;
        }
        public void Shift(Vector3 shift)
        {
            transform.Translate(shift);
        }
        public void MoveTo(Vector3 position)
        {
            position.z = 0;
            transform.position = position;
        }

        private void Update()
        {
            for (int i = 0; i < _receivers.Count; i++)
            {
                ExertInfluence(_receivers[i]);
            }
        }
        private void ExertInfluence(GravityReceiver receiver)
        {
            receiver.ReceiveInfluence(this);
        }
    }
}
