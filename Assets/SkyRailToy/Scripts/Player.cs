using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SkyRailToy
{
    public class Player : MonoBehaviour, SkyRailActions.IDefaultActions
    {
        [SerializeField]
        private Hero _hero;
        [SerializeField]
        private SkyObject _heroSkyObject;
        [SerializeField]
        private SkyRail _rail;
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

        public void OnMove(InputAction.CallbackContext context)
        {
            float analog = context.ReadValue<float>();
            int direction = 0;
            if (analog < -0.1f)
            {
                direction = -1;
            }
            if (analog > 0.1f)
            {
                direction = 1;
            }
            _hero.SetLateralDirection(direction);
        }
        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _hero.StartJumping();
                _rail.Catch(_heroSkyObject);
            }
            if (context.canceled)
            {
                _hero.StopJumping();
            }
        }
        public void OnFire(InputAction.CallbackContext context)
        {

        }
        public void OnDrop(InputAction.CallbackContext context)
        {

        }
    }
}