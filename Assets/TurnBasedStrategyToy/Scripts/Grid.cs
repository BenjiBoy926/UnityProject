using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

namespace TurnBasedStrategyToy
{
    public class Grid : MonoBehaviour
    {
        [SerializeField]
        private Camera _screenToWorld;
        [SerializeField, ReadOnly]
        private List<ObjectOnGrid> _occupants = new List<ObjectOnGrid>();

        public void Register(ObjectOnGrid obj)
        {
            if (_occupants.Contains(obj))
            {
                return;
            }
            _occupants.Add(obj);
        }
        public void Unregister(ObjectOnGrid obj)
        {
            _occupants.Remove(obj);
        }
        public IEnumerable<ObjectOnGrid> GetOccupants(Vector2Int gridPosition)
        {
            return _occupants.Where(x => x.Position == gridPosition);
        }

        public Vector2Int ScreenToGrid(Vector2 screen)
        {
            return WorldToGrid(_screenToWorld.ScreenToWorldPoint(screen));
        }
        public Vector3 GridToWorld(Vector2Int gridPosition)
        {
            return new Vector3(gridPosition.x, gridPosition.y, 0);
        }
        public Vector2Int WorldToGrid(Vector3 worldPosition)
        {
            return new Vector2Int(Mathf.RoundToInt(worldPosition.x), Mathf.RoundToInt(worldPosition.y));
        }
    }
}