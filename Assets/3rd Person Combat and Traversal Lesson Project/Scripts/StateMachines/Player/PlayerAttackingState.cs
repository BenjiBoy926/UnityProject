using UnityEngine;

public class PlayerAttackingState : PlayerState
{
    private float ElapsedTime => Time.time - _startTime;

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
        if (ElapsedTime > _attack.TotalDuration)
        {
            HandleAttackEnd();
        }
    }
    private void HandleAttackEnd()
    {
        if (Machine.IsAttackBuffered)
        {
            Machine.SetState(new PlayerAttackingState(Machine, _attackIndex + 1));
        }
        else
        {
            Machine.SetState(new PlayerFreeLookState(Machine));
        }
    }

    public override void Exit()
    {
        
    }
}