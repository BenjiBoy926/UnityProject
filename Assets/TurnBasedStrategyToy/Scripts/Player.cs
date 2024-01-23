using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TurnBasedStrategyToy
{
    public class Player : MonoBehaviour, TurnBasedStrategyToyActions.IDefaultActions
    {
        [SerializeField]
        private Cursor _cursor;
        private TurnBasedStrategyToyActions _actions;

        private void Awake()
        {
            _actions = new TurnBasedStrategyToyActions();
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

        public void OnCursorPosition(InputAction.CallbackContext context)
        {
            Vector2 screenPoint = context.ReadValue<Vector2>();
            _cursor.SnapToGridPointClosestToScreenPoint(screenPoint);
        }
        public void OnCursorSelect(InputAction.CallbackContext context)
        {

        }
    }
}