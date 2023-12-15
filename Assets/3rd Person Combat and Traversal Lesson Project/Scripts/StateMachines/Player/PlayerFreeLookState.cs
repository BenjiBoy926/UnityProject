using UnityEngine;

public class PlayerFreeLookState : PlayerState
{
    private bool _isMovementVectorNonZero;

    public PlayerFreeLookState(PlayerStateMachine machine) : base(machine) 
    { 

    }

    public override void Enter()
    {
        Machine.MovementVectorChanged += OnMovementVectorChanged;
        Machine.BlendToFreeLookAnimationState(0.1f);
        RefreshIsMovementVectorNonZero();
    }
    public override void Tick()
    {
        if (Machine.IsAttackBuffered)
        {
            Machine.SetState(new PlayerAttackingState(Machine, 0));
            return;
        }
        if (_isMovementVectorNonZero)
        {
            Machine.TurnTowardsDirection(Machine.MovementVector);
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