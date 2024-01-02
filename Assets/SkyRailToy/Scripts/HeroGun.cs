using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;

namespace SkyRailToy
{
    public class HeroGun : MonoBehaviour
    {
        [SerializeField]
        private HeroStats _stats;
        [SerializeField]
        private HeroBullet _bulletPrefab;
        [SerializeField]
        private HeroGunNozzle _leftNozzle;
        [SerializeField]
        private HeroGunNozzle _rightNozzle;
        private Cooldown _cooldown;

        private void Awake()
        {
            _cooldown = new Cooldown(_stats.FireRate); 
        }

        [Button("Fire Left")]
        public void FireLeft()
        {
            FireFromNozzle(_leftNozzle);
        }
        [Button("Fire Right")]
        public void FireRight()
        {
            FireFromNozzle(_rightNozzle);
        }

        private void FireFromNozzle(HeroGunNozzle nozzle)
        {
            if (!_cooldown.IsCooledDown)
            {
                return;
            }
            nozzle.Fire(_bulletPrefab);
            _cooldown.Start();
        }
    }
}
