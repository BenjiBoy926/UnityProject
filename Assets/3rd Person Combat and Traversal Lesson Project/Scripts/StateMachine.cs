using UnityEngine;

public class StateMachine : MonoBehaviour
{
    // Codey: use this public property in case we want to change to using some other delta liked fixedDeltaTime
    public float DeltaTime => Time.deltaTime;

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
        _currentState.Tick();
    }
}