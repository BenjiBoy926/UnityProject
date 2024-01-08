using System.Collections;
using UnityEngine;
using Core;

namespace GravityToy
{
    public class TargetSwarm : MonoBehaviour
    {
        [SerializeField]
        private Target _targetPrefab;
        [SerializeField]
        private float _spawnRate = 1;
        private Cooldown _spawnCooldown;
        private Boundary _boundary;

        private void Awake()
        {
            _spawnCooldown = new Cooldown(_spawnRate);
            if (_boundary == null)
            {
                _boundary = FindFirstObjectByType<Boundary>();
            }
        }
        private void Update()
        {
            if (_spawnCooldown.IsCooledDown)
            {
                Spawn();
            }
        }
        private void Spawn()
        {
            Vector2 point = _boundary.GetRandomPointOnRandomEdge();
            Target target = Instantiate(_targetPrefab, point, Quaternion.identity, transform);
            _spawnCooldown.Start();
        }
    }
}