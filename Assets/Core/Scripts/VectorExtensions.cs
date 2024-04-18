using UnityEngine;

namespace Core
{
    public static class VectorExtensions
    {
        public static float Get(this Vector2 vector, Dimension dimension)
        {
            return vector[(int)dimension];
        }
        public static Vector2 Set(this Vector2 vector, float value, Dimension dimension)
        {
            vector[(int)dimension] = value;
            return vector;
        }
    }
}