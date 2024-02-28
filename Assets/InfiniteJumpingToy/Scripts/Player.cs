using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace InfinitJumpingToy
{
    public class Player : MonoBehaviour, InfiniteJumpingToyActions.IDefaultActions
    {
        [SerializeField]
        private HeroJump _jump;
        private InfiniteJumpingToyActions _actions;

        private void Awake()
        {
            _actions = new();
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

        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _jump.StartJumping();
            }
            if (context.canceled)
            {
                _jump.StopJumping();
            }
        }
    }
}