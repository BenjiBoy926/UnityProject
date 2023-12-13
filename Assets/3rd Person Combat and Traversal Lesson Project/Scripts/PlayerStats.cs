using UnityEngine;

[CreateAssetMenu(menuName = "3rd Person/Player Stats")]
public class PlayerStats : ScriptableObject
{
    public float MaxDegreeTurnPerSecond => 180 / _timeToTurnAround;
    public float IdleToWalkTransitionTime => _idleToWalkTransitionTime;
    public float AttackTransitionTime => _attackTransitionTime;
    public int ComboLength => _attackCombo.Length;

    [SerializeField]
    private float _timeToTurnAround = 0.5f;
    [SerializeField]
    private float _idleToWalkTransitionTime = 0.1f;
    [SerializeField]
    private AttackInfo[] _attackCombo;
    [SerializeField]
    private float _attackTransitionTime = 0.1f;

    public AttackInfo GetAttack(int index)
    {
        return _attackCombo[index];
    }
}