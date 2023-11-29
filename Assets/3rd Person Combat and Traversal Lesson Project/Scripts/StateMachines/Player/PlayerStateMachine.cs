using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    [SerializeField]
    private PlayerInputInterface _input;

    protected override void Awake()
    {
        base.Awake();
        _input.Jumped += OnJump;
        _input.Dodged += OnDodge;
    }
    private void OnDestroy()
    {
        _input.Jumped -= OnJump;
        _input.Dodged -= OnDodge;
    }

    private void OnJump()
    {
        SetState(new PlayerTestState(this, "Jump"));
    }
    private void OnDodge()
    {
        SetState(new PlayerTestState(this, "Dodge"));
    }
}