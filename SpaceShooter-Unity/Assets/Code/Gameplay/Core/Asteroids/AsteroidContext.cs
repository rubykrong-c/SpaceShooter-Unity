using UnityEngine;

namespace Code.Gameplay.Core
{
    public readonly struct AsteroidContext
    {
        public readonly Transform Transform;
        public readonly float Speed;

        public AsteroidContext(Transform transform, float speed)
        {
            Transform = transform;
            Speed = speed;
        }
    }
}