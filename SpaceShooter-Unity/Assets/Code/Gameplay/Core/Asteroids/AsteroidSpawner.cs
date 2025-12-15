using System;
using System.Collections.Generic;
using Code.Base.Pool;
using RotaryHeart.Lib.SerializableDictionary;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.Gameplay.Core
{
    public class AsteroidSpawner : IAsteroidSpawner
    {
        private readonly PoolingSystem _poolingSystem;
        private readonly Settings _settings;
        private readonly IAsteroidMovementResolver _movementResolver;
        private readonly LevelContainer _levelContainer;

        private List<Vector2> _spawnPointList = new List<Vector2>();
        public AsteroidSpawner(
            PoolingSystem poolingSystem, 
            Settings settings,
            IAsteroidMovementResolver movementResolver,
            LevelContainer levelContainer)
        {
            _poolingSystem = poolingSystem;
            _settings = settings;
            _movementResolver = movementResolver;
            _levelContainer = levelContainer;
            
            _spawnPointList = _levelContainer.GetSpawnPointsPos();
        }
        
        public void Spawn(EAsteroidType type)
        {
            var obj = CreateAsteroid(type);
            obj.transform.position = GetRandomSpawnPoint();
        }

        public void Despawn(GameObject asteroid)
        {
            _poolingSystem.DestroyAPS(asteroid);
        }

        private AsteroidBehaviour CreateAsteroid(EAsteroidType type)
        {
            AsteroidData data = null;
            _settings._dataMaterials?.TryGetValue(type, out data);
            AsteroidBehaviour element = _poolingSystem.InstantiateAPS(_settings.ElementPoolKey).GetComponent<AsteroidBehaviour>();
            var movement = _movementResolver.Resolve(type);
            element.Configure(type, data, movement);
            
            return element;
        }

        private Vector2 GetRandomSpawnPoint()
        {
            var randomIndex = Random.Range(0, _spawnPointList.Count);
            return _spawnPointList[randomIndex];
        }

        [Serializable]
        public class Settings
        {
            public string ElementPoolKey = "Asteroid";
            public AsteroidDataDictionary _dataMaterials;
        }

        [Serializable]
        public class AsteroidDataDictionary : SerializableDictionaryBase<EAsteroidType, AsteroidData>
        {
        }

        [Serializable]
        public class AsteroidData
        {
            public Color ColorAsteroid;
            public float Speed;
        }
        
    }
}