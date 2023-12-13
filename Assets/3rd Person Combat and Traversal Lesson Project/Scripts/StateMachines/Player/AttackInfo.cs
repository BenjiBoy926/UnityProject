using UnityEngine;

[System.Serializable]
public struct AttackInfo
{
    public string AnimationName => _animationName;
    public float TransitionDuration => _transitionDuration;
    public float Duration => _duration;
    public float TotalDuration => _transitionDuration + _duration;

    [SerializeField]
    private string _animationName;
    [SerializeField]
    private float _transitionDuration;
    [SerializeField]
    private float _duration;
}