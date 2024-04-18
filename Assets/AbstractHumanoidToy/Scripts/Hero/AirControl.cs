using System;
using UnityEngine;

namespace AbstractHumanoidToy
{
    [Serializable]
    public struct AirControl
    {
        [SerializeField]
        private float _forwardForce;
        [SerializeField]
        private float _backwardForce;
        [SerializeField]
        private float _forwardTopSpeed;
        [SerializeField]
        private float _backwardTopSpeed;

        public AirControl(float forwardForce, float backwardForce, float forwardTopSpeed, float backwardTopSpeed)
        {
            _forwardForce = forwardForce;
            _backwardForce = backwardForce;
            _forwardTopSpeed = forwardTopSpeed;
            _backwardTopSpeed = backwardTopSpeed;
        }
        public void ApplyToBody(Rigidbody2D body, float direction)
        {

        }
    }
}