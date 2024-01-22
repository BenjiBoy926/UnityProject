using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace TurnBasedStrategyToy
{
    public class Grid : MonoBehaviour
    {
        private static List<ObjectOnGrid> _occupants = new List<ObjectOnGrid>();

        public static void Register(ObjectOnGrid obj)
        {
            if (_occupants.Contains(obj))
            {
                return;
            }
            _occupants.Add(obj);
        }
        public static void Unregister(ObjectOnGrid obj)
        {
            _occupants.Remove(obj);
        }
        public static IEnumerable<ObjectOnGrid> GetOccupants(Vector2Int gridPosition)
        {
            return _occupants.Where(x => x.Position == gridPosition);
        }

        public static Vector3 GridPositionToWorldPosition(Vector2Int gridPosition)
        {
            return new Vector3(gridPosition.x, gridPosition.y, 0);
        }
        public static Vector2Int WorldPositionToGridPosition(Vector3 worldPosition)
        {
            return new Vector2Int(Mathf.RoundToInt(worldPosition.x), Mathf.RoundToInt(worldPosition.y));
        }
    }
}