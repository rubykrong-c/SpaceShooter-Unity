using System;
using System.Collections.Generic;
using Code.Base.Pool;
using UnityEngine;

namespace Code.Gameplay.Core.Ship.Bullet
{
    public class BulletSpawner: IBulletSpawner
    {
        private readonly PoolingSystem _poolingSystem;
        private readonly Settings _settings;
        private readonly LinearForwardMovement.Factory _movementFactory;
       
        public BulletSpawner(
            PoolingSystem poolingSystem, 
            Settings settings,
            LinearForwardMovement.Factory movementFactory)
        {
            _poolingSystem = poolingSystem;
            _settings = settings;
            _movementFactory = movementFactory;
        }
        
        public GameObject Spawn(Vector3 startPos, Vector2 minBounds, Vector2 maxBounds, Vector2 direction)
        {
            var bullet = _poolingSystem.InstantiateAPS(_settings.ElementPoolKey).GetComponent<BulletBehaviour>();
            var movement = _movementFactory.Create();
            
            bullet.Configure(movement, _settings.Speed,  minBounds, maxBounds, direction, Despawn);
            bullet.SetCollisionHandler(Despawn);
            bullet.transform.position = startPos;
            return bullet.gameObject;
        }
        
        public void Despawn(GameObject bullet) => _poolingSystem.DestroyAPS(bullet);
        
        [Serializable]
        public class Settings
        {
            public string ElementPoolKey = "Bullet";
            public float Speed = 1f;
        }
    }
}