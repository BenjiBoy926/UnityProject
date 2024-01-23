using System.Collections;
using UnityEngine;

namespace TurnBasedStrategyToy
{
    [CreateAssetMenu(menuName = "Turn Based Strategy Toy/Battle Unit Stats")]
    public class BattleUnitStats : ScriptableObject
    {
        public int Movement => _movement;
        public int Range => _range;

        [SerializeField]
        private int _movement = 3;
        [SerializeField]
        private int _range = 1;
    }
}