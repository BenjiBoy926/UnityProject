using UnityEngine;

public class PlayerTestState : PlayerState
{
    private string _name;
    private bool _isMovementVectorNonZero;

    public PlayerTestState(PlayerStateMachine machine, string name) : base(machine) 
    { 
        _name = name;
    }

    public override void Enter()
    {
        Machine.MovementVectorChanged += OnMovementVectorChanged;
        RefreshIsMovementVectorNonZero();
    }
    public override void Tick()
    {
        if (_isMovementVectorNonZero)
        {
            Machine.Move(Machine.MovementVector);
            Machine.BlendTowardsWalkingAnimation();
        }
        else
        {
            Machine.BlendTowardsIdleAnimation();
        }
    }
    public override void Exit()
    {
        Machine.MovementVectorChanged -= OnMovementVectorChanged;
    }

    private void OnMovementVectorChanged()
    {
        RefreshIsMovementVectorNonZero();
    }
    private void RefreshIsMovementVectorNonZero()
    {
        _isMovementVectorNonZero = Machine.IsMovementVectorNonZero;
    }
}