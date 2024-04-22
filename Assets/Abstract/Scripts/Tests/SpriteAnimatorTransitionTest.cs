using NaughtyAttributes;
using UnityEngine;

namespace Abstract
{
    public class SpriteAnimationTransitionTest : MonoBehaviour
    {
        [SerializeField]
        private SpriteAnimator _animator;
        [SerializeField]
        private SpriteAnimation _animation1;
        [SerializeField]
        private SpriteAnimation _animation2;

        [Button]
        private void TransitionToAnimation1()
        {
            _animator.TransitionTo(_animation1, 1);
        }
        [Button]
        private void TransitionToAnimation2()
        {
            _animator.TransitionTo(_animation2, 1);
        }
    }
}