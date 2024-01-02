using System.Collections;
using UnityEngine;

namespace SkyRailToy
{
    public class SkyRail : MonoBehaviour
    {
        [SerializeField]
        private SkyObject _target;

        private void OnValidate()
        {
            ReflectTarget();
        }
        private void OnEnable()
        {
            ReflectTarget();
        }

        public void Catch(SkyObject target)
        {
            SetTarget(target);
        }
        public void Release()
        {
            SetTarget(null);
        }

        private void SetTarget(SkyObject target)
        {
            _target = target;
            ReflectTarget();
        }
        private void ReflectTarget()
        {
            if (_target != null)
            {
                transform.position = _target.CatchPosition;
            }
        }
    }
}