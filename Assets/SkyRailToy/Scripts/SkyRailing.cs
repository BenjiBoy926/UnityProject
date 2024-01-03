using NaughtyAttributes;
using System.Collections;
using UnityEngine;

namespace SkyRailToy
{
    public class SkyRailing : MonoBehaviour
    {
        [SerializeField, ReadOnly]
        private SkyRail[] _rails;
        [SerializeField]
        private SkyRail _railPrefab;
        [SerializeField]
        private int _railCount = 6;
        [SerializeField]
        private float _offsetBetweenRails = 1.5f;
        [SerializeField, Range(0, 1)]
        private float _railColorSaturation = 0.6f;
        [SerializeField, Range(0, 1)]
        private float _railColorValue = 1;
        [SerializeField, Range(0, 1)]
        private float _railColorAlpha = 0.1f;

        private void Awake()
        {
            _rails = new SkyRail[_railCount];
            for (int i = 0; i < _railCount; i++)
            {
                CreateSkyRail(i);
            }
        }
        private void CreateSkyRail(int index)
        {
            SkyRail rail = Instantiate(_railPrefab, transform);
            rail.SetLocalPosition(GetRailLocalPosition(index));
            rail.SetColor(GetRailColor(index));
            _rails[index] = rail;  
        }
        private Vector3 GetRailLocalPosition(int index)
        {
            return index * _offsetBetweenRails * Vector3.up; 
        }
        private Color GetRailColor(int index)
        {
            float hue = index / (float)_railCount;
            Color color = Color.HSVToRGB(hue, _railColorSaturation, _railColorValue);
            color.a = _railColorAlpha;
            return color;
        }

        public void Catch()
        {

        }
        public void Release()
        {

        }
    }
}