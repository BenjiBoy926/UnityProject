using UnityEngine;

[System.Serializable]
public struct AttackInfo
{
    public string AnimationName => _animationName;
    public float TransitionDuration => _transitionDuration;
    private float AnimationDuration => _animationDuration;
    public float TimeBeforeNextAttack => _transitionDuration + _timeBeforeNextAttack;
    public float TotalDuration => _transitionDuration + _animationDuration;

    [SerializeField]
    private string _animationName;
    [SerializeField]
    private float _transitionDuration;
    [SerializeField]
    private float _animationDuration;
    [SerializeField]
    private float _timeBeforeNextAttack;
}