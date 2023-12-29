using System.Collections;
using UnityEngine;

namespace GunToy
{
    public class AttackGun : MonoBehaviour
    {
        [SerializeField]
        private MuzzleFlash _muzzleFlash;
        [SerializeField]
        private float _minimumTimeBetweenBullets = 0.1f;

        public void Fire()
        {
            _muzzleFlash.Play();
        }
    }
}