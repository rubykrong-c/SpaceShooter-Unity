using System;

namespace Code.Gameplay.Core.Ship.Bullet
{
    public interface IBulletMovement
    {
        void Start(BulletContext context, Action despawn);
        void Tick(float deltaTime);
        void Stop();
    }
}