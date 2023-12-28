using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GravityToy
{
    public class InputInterface : MonoBehaviour, GravityToyActions.IDefaultActions
    {
        public Vector3 WorldGravityPosition => _mouseConversionCamera.ScreenToWorldPoint(_gravityPosition);
        public bool IsGravityActive => _isGravityActive;

        [SerializeField]
        private Camera _mouseConversionCamera;
        private GravityToyActions _actions;
        private Vector2 _gravityPosition;
        private bool _isGravityActive;

        public event Action GravityIsActiveChanged = delegate { };
        public event Action GravityPositionChanged = delegate { };

        private void Awake()
        {
            _actions = new GravityToyActions();
            _actions.Default.SetCallbacks(this);
        }
        private void OnEnable()
        {
            _actions.Enable();
        }
        private void OnDisable()
        {
            _actions.Disable();
        }

        public void OnIsGravityActive(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _isGravityActive = true;
            }
            if (context.canceled)
            {
                _isGravityActive = false;
            }
            GravityIsActiveChanged();
        }
        public void OnGravityPosition(InputAction.CallbackContext context)
        {
            _gravityPosition = context.ReadValue<Vector2>();
            GravityPositionChanged();
        }
    }
}
