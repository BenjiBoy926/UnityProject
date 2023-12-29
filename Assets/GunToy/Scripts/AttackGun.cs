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
        [SerializeField]
        private float _range = 10;

        public void Fire()
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, _range);
            if (hit.transform != null)
            {
                if (hit.transform.TryGetComponent(out Target target))
                {
                    target.ReceiveHit(hit);
                }
            }
            _muzzleFlash.Play();
        }
    }
}