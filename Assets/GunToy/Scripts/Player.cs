using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GunToy
{
    public class Player : MonoBehaviour, GunToyActions.IDefaultActions
    {
        [SerializeField]
        private Camera _mouseToWorldCamera;
        [SerializeField]
        private Reticle _reticle;
        [SerializeField]
        private AttackGun _attackGun;
        [SerializeField]
        private RecoilGun _recoilGun;
        [SerializeField]
        private HeroLegs _legs;
        private GunToyActions _actions;

        private void Awake()
        {
            _actions = new GunToyActions();
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
            float direction = context.ReadValue<float>();
            _legs.SetCurrentMoveDirection(direction);
        }
        public void OnAimPoint(InputAction.CallbackContext context)
        {
            Vector2 point = context.ReadValue<Vector2>();
            point = _mouseToWorldCamera.ScreenToWorldPoint(point);
            _reticle.SetPoint(point);
        }
        public void OnAimDirection(InputAction.CallbackContext context)
        {
            Vector2 direction = context.ReadValue<Vector2>();
            _reticle.SetDirection(direction);
        }
        public void OnRecoil(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _recoilGun.Fire();
            }
        }
    }
}
