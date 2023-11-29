using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private IState _currentState;

    protected virtual void Awake()
    {
        RefreshIsEnabled();
    }
    public void SetState(IState newState)
    {
        if (_currentState != null)
        {
            _currentState.Exit();
        }
        _currentState = newState;
        if (_currentState != null )
        {
            _currentState.Enter();
        }
        RefreshIsEnabled();
    }
    private void RefreshIsEnabled()
    {
        enabled = ShouldBeEnabled();
    }
    private bool ShouldBeEnabled()
    {
        return _currentState != null;
    }
    private void Update()
    {
        _currentState.Tick(Time.deltaTime);
    }
}