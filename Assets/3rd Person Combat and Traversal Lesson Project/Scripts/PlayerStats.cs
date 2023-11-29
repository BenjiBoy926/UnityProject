using UnityEngine;

[CreateAssetMenu(menuName = "3rd Person/Player Stats")]
public class PlayerStats : ScriptableObject
{
    public float MaxDegreeTurnPerSecond => 180 / _timeToTurnAround;

    [SerializeField]
    private float _timeToTurnAround = 0.5f;
}