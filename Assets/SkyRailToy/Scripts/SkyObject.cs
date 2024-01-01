using System.Collections;
using UnityEngine;

namespace SkyRailToy
{
    public class SkyObject : MonoBehaviour
    {
        public Vector3 CatchPosition => _catchPosition.position;

        [SerializeField]
        private Transform _catchPosition;
    }
}