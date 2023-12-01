using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private Vector3 TargetPosition => _cameraTarget.position + _followOffset;

    [SerializeField]
    private Transform _cameraTarget;
    [SerializeField]
    private Vector3 _followOffset = Vector3.right;
    [SerializeField]
    private float _maxFollowDistancePerSecond = 1;

    private void OnValidate()
    {
        if (_cameraTarget == null)
        {
            return;
        }
        transform.position = TargetPosition;
    }
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, TargetPosition, _maxFollowDistancePerSecond * Time.deltaTime);
    }
}