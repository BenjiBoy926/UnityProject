using UnityEngine;

public class PlayerAttackingState : PlayerState
{
    private float ElapsedTime => Time.time - _startTime;
    private int NextAttackIndex => _attackIndex + 1;
    private bool HasNextAttack => NextAttackIndex < Machine.ComboLength;

    private int _attackIndex;
    private AttackInfo _attack;
    private float _startTime;
    private bool _hasPerformedLeap = false;
    private Vector3 _attackVelocity;

    public PlayerAttackingState(PlayerStateMachine machine, int attackIndex) : base(machine) 
    { 
        _attackIndex = attackIndex;
        _attack = machine.GetAttack(attackIndex);
        _startTime = Time.time;

        Vector2 movementVector = machine.InputMovementVector;
        if (Mathf.Abs(movementVector.x) > 0.01f || Mathf.Abs(movementVector.y) > 0.01f)
        {
            _attackVelocity = machine.WorldMovementVector * _attack.LeapSpeed;
        }
        else
        {
            _attackVelocity = machine.Heading * _attack.LeapSpeed;
        }
    }

    public override void Enter()
    {
        Machine.ClearAttackBuffer();
        Machine.BlendToAttackAnimation(_attackIndex);
        Machine.SetRootMotionActive(false);
    }
    public override void Tick()
    {
        Machine.TurnTowardsDirection(_attackVelocity, _attack.TransitionDuration);
        if (ShouldPerformAttackLeap())
        {
            Machine.Leap(_attackVelocity);
            _hasPerformedLeap = true;
        }
        if (ShouldTransitionToNextAttack())
        {
            Machine.SetState(new PlayerAttackingState(Machine, NextAttackIndex));
        }
        if (ElapsedTime > _attack.TotalDuration)
        {
            Machine.SetState(new PlayerFreeLookState(Machine));
        }
    }
    private bool ShouldPerformAttackLeap()
    {
        return ElapsedTime > _attack.TimeOfAttackLeap && !_hasPerformedLeap;
    }
    private bool ShouldTransitionToNextAttack()
    {
        return ElapsedTime > _attack.TimeBeforeNextAttack && Machine.IsAttackBuffered && HasNextAttack;
    }

    public override void Exit()
    {
        
    }
}