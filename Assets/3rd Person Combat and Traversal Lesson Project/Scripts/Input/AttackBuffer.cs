public struct AttackBuffer
{
    public bool IsAttackBuffered => _isAttackBuffered;

    private bool _isAttackBuffered;

    public void BufferAttack()
    {
        _isAttackBuffered = true;
    }
    public void Clear()
    {
        _isAttackBuffered = false;
    }
}