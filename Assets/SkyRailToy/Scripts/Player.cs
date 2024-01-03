using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SkyRailToy
{
    public class Player : MonoBehaviour, SkyRailActions.IDefaultActions
    {
        [SerializeField]
        private HeroBody _heroBody;
        [SerializeField]
        private HeroGun _heroGun;
        [SerializeField]
        private SkyRail _rail;
        [SerializeField]
        private int _shootingDirection = 0;
        private SkyRailActions _actions;

        private void Awake()
        {
            _actions = new SkyRailActions();
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
        private void Update()
        {
            if (_shootingDirection == 1)
            {
                _heroGun.FireRight();
            }
            if (_shootingDirection == -1)
            {
                _heroGun.FireLeft();
            }
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            int direction = GetRawAxis(context);
            _heroBody.SetLateralDirection(direction);
        }
        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _heroBody.StartJumping();
                _rail.Catch();
            }
            if (context.canceled)
            {
                _heroBody.StopJumping();
            }
        }
        public void OnFire(InputAction.CallbackContext context)
        {
            _shootingDirection = GetRawAxis(context);
        }
        public void OnDrop(InputAction.CallbackContext context)
        {
            if (_heroBody.IsJumping)
            {
                return;
            }
            if (context.performed)
            {
                _rail.Release();
            }
            if (context.canceled)
            {
                _rail.Catch();
            }
        }

        private int GetRawAxis(InputAction.CallbackContext context)
        {
            float axis = context.ReadValue<float>();
            if (axis > 0.1f)
            {
                return 1;
            }
            if (axis < -0.1f)
            {
                return -1;
            }
            return 0;
        }
    }
}