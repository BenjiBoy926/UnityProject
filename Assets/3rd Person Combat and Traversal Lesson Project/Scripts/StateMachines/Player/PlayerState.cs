public abstract class PlayerState : IState
{
    protected PlayerStateMachine Machine => _machine;
    private PlayerStateMachine _machine;

    public PlayerState(PlayerStateMachine machine)
    {
        _machine = machine;
    }

    public abstract void Enter();
    public abstract void Tick(float deltaTime);
    public abstract void Exit();
}