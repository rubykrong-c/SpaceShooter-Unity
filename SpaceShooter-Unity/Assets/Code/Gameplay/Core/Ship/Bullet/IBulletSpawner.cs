using UnityEngine;

namespace Code.Gameplay.Core.Ship.Bullet
{
    public interface IBulletSpawner
    {
        GameObject Spawn(Vector3 startPos, Vector2 minBounds, Vector2 maxBounds, Vector2 direction);
        void Despawn(GameObject bullet);
    }
}