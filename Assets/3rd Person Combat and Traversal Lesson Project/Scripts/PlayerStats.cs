using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "3rd Person/Player Stats")]
public class PlayerStats : ScriptableObject
{
    public float TimeToTurnAroundWhileWalking => _timeToTurnAroundWhileWalking;
    public float IdleToWalkTransitionTime => _idleToWalkTransitionTime;
    public int ComboLength => _attackCombo.Length;

    [SerializeField]
    [FormerlySerializedAs("_timeToTurnAround")]
    private float _timeToTurnAroundWhileWalking = 0.5f;
    [SerializeField]
    private float _idleToWalkTransitionTime = 0.1f;
    [SerializeField]
    private AttackInfo[] _attackCombo;

    public AttackInfo GetAttack(int index)
    {
        return _attackCombo[index];
    }
}