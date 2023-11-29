using UnityEngine;

[CreateAssetMenu(menuName = "3rd Person/Player Stats")]
public class PlayerStats : ScriptableObject
{
    public float MoveSpeed => _moveSpeed;

    [SerializeField]
    private float _moveSpeed = 5;
}