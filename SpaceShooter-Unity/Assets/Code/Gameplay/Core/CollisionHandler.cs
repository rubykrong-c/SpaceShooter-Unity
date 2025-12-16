using System;
using Code.Gameplay.Core.Ship.Bullet;
using Zenject;

namespace Code.Gameplay.Core
{
    //TODO: убрать это класс
    public class CollisionHandler : ICollisionHandler, IDisposable
    {
  
        private readonly IAsteroidSpawner _asteroidSpawner;
        
        public CollisionHandler(
             
            IAsteroidSpawner asteroidSpawner)
        {
            _asteroidSpawner = asteroidSpawner;

            _asteroidSpawner.OnAsteroidDestroy += OnAsteroidHitBulletOrShip;
        }

        public void OnAsteroidHitBulletOrShip()
        {
            //_asteroidSpawner.Despawn(asteroid.gameObject);
        }
        
        public void Dispose()
        {
            _asteroidSpawner.OnAsteroidDestroy -= OnAsteroidHitBulletOrShip;
        }
 
    }
}