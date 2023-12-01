using UnityEngine;

[CreateAssetMenu(menuName = "3rd Person/Player Stats")]
public class PlayerStats : ScriptableObject
{
    public float MaxDegreeTurnPerSecond => 180 / _timeToTurnAround;
    public float IdleToWalkTransitionTime => _idleToWalkTransitionTime;

    [SerializeField]
    private float _timeToTurnAround = 0.5f;
    [SerializeField]
    private float _idleToWalkTransitionTime = 0.1f;
}