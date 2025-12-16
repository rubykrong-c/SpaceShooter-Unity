using UnityEngine;

namespace Code.Gameplay.Core.Ship.Bullet
{
    public readonly struct BulletContext
    {
        public readonly Transform Transform;
        public readonly float Speed;
        public readonly Vector2 MinBounds;
        public readonly Vector2 MaxBounds;
        public readonly Vector2 Direction;

        public BulletContext(
            Transform transform, 
            float speed, 
            Vector2 minBounds, 
            Vector2 maxBounds,
            Vector2 direction)
        {
            Transform = transform;
            Speed = speed;
            MinBounds = minBounds;
            MaxBounds = maxBounds;
            Direction = direction;
        }
    }
}