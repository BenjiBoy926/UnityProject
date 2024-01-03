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
        private ObjectOnRail _heroOnRail;
        [SerializeField]
        private Vector2Int _movementDirection;
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
            UpdateObjectOnRailPosition();
        }
        private void UpdateObjectOnRailPosition()
        {
            if (_heroOnRail.IsMoving)
            {
                return;
            }
            if (_movementDirection.y > 0)
            {
                _heroOnRail.MoveUp();
            }
            if (_movementDirection.y < 0)
            {
                _heroOnRail.MoveDown();
            }
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            Vector2 axis = context.ReadValue<Vector2>();
            _movementDirection = GetRawAxis(axis);
            _heroBody.SetLateralDirection(_movementDirection.x);
        }
        public void OnFire(InputAction.CallbackContext context)
        {
            _shootingDirection = GetRawAxis(context.ReadValue<float>());
        }

        private Vector2Int GetRawAxis(Vector2 axis)
        {
            return new Vector2Int(
                GetRawAxis(axis.x),
                GetRawAxis(axis.y));
        }
        private int GetRawAxis(float axis)
        {
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