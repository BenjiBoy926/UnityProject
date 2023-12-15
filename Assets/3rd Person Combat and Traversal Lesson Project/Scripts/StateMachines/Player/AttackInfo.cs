using UnityEngine;

[System.Serializable]
public struct AttackInfo
{
    public string AnimationName => _animationName;
    public float TransitionDuration => _transitionDuration;
    public float TimeBeforeNextAttack => _transitionDuration + _timeBeforeNextAttack;
    public float TotalDuration => _transitionDuration + _animationDuration;
    public float TimeOfAttackLeap => _transitionDuration + _timeOfAttackLeap;
    public float LeapSpeed => _leapSpeed;

    [SerializeField]
    private string _animationName;
    [SerializeField]
    private float _transitionDuration;
    [SerializeField]
    private float _animationDuration;
    [SerializeField]
    private float _timeBeforeNextAttack;
    [SerializeField]
    private float _timeOfAttackLeap;
    [SerializeField]
    private float _leapSpeed;
}