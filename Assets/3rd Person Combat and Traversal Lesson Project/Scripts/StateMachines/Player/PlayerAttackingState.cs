using UnityEngine;

public class PlayerAttackingState : PlayerState
{
    private float ElapsedTime => Time.time - _startTime;
    private int NextAttackIndex => _attackIndex + 1;
    private bool HasNextAttack => NextAttackIndex < Machine.ComboLength;

    private int _attackIndex;
    private AttackInfo _attack;
    private float _startTime;

    public PlayerAttackingState(PlayerStateMachine machine, int attackIndex) : base(machine) 
    { 
        _attackIndex = attackIndex;
        _attack = machine.GetAttack(attackIndex);
        _startTime = Time.time;
    }

    public override void Enter()
    {
        Machine.ClearAttackBuffer();
        Machine.BlendToAttackAnimation(_attackIndex);
    }
    public override void Tick()
    {
        if (ElapsedTime > _attack.TimeBeforeNextAttack && Machine.IsAttackBuffered && HasNextAttack)
        {
            Machine.SetState(new PlayerAttackingState(Machine, NextAttackIndex));
        }
        if (ElapsedTime > _attack.TotalDuration)
        {
            Machine.SetState(new PlayerFreeLookState(Machine));
        }
    }
    private bool ShouldTransitionToNextAttack()
    {
        return ElapsedTime > _attack.TimeBeforeNextAttack && Machine.IsAttackBuffered && HasNextAttack;
    }

    public override void Exit()
    {
        
    }
}