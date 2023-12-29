using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GravityToy
{
    public class Player : MonoBehaviour, GravityToyActions.IDefaultActions
    {
        public Vector3 WorldGravityPosition => _mouseConversionCamera.ScreenToWorldPoint(_inputGravityPosition);
        public bool IsGravityActive => _isGravityActive;

        [SerializeField]
        private Camera _mouseConversionCamera;
        [SerializeField]
        private GravityWell _well;
        [SerializeField]
        private bool _isGravityActive;
        [SerializeField]
        private float _moveSpeed = 5;
        [SerializeField]
        private Vector2 _gravityInputMovement;
        private Vector2 _inputGravityPosition;
        private GravityToyActions _actions;

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
            ReflectIsGravityActive();
        }
        private void OnDisable()
        {
            _actions.Disable();
        }
        private void Update()
        {
            _well.Shift(_gravityInputMovement * _moveSpeed * Time.deltaTime);
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
            ReflectIsGravityActive();
        }
        public void OnGravityPosition(InputAction.CallbackContext context)
        {
            _inputGravityPosition = context.ReadValue<Vector2>();
            _well.MoveTo(WorldGravityPosition);
        }
        public void OnGravityMove(InputAction.CallbackContext context)
        {
            _gravityInputMovement = context.ReadValue<Vector2>();
        }

        private void ReflectIsGravityActive()
        {
            _well.SetIsEnabled(_isGravityActive);
        }
    }
}
