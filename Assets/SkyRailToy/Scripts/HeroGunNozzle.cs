using UnityEngine;

namespace SkyRailToy
{
    public class HeroGunNozzle : MonoBehaviour
    {
        [SerializeField]
        private HeroStats _stats;

        public void Fire(HeroBullet bullet)
        {
            bullet = Instantiate(bullet, transform.position, transform.rotation);
            bullet.Send(_stats.BulletSpeed);
            Destroy(bullet.gameObject, _stats.BulletLifetime);
        }
    }
}