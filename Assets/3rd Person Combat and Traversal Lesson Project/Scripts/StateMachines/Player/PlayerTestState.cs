using UnityEngine;

public class PlayerTestState : PlayerState
{
    private string _name;

    public PlayerTestState(PlayerStateMachine machine, string name) : base(machine) 
    { 
        _name = name;
    }

    public override void Enter()
    {
        Debug.Log($"Entered the test state: {_name}");
    }
    public override void Tick(float deltaTime)
    {

    }
    public override void Exit()
    {
        Debug.Log($"Exited the test state: {_name}");
    }
}