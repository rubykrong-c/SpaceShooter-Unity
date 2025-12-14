using System;
using Code.Base.Pool;
using RotaryHeart.Lib.SerializableDictionary;
using UnityEngine;

namespace Code.Gameplay.Core
{
    public class AsteroidSpawner : IAsteroidSpawner
    {
        private readonly PoolingSystem _poolingSystem;
        private readonly Settings _settings;
        
        public AsteroidSpawner(PoolingSystem poolingSystem, Settings settings)
        {
            _poolingSystem = poolingSystem;
            _settings = settings;
        }
        
        public void Spawn(EAsteroidType type)
        {
            Debug.Log($"Spawn {type}");
            CreateStack(type);
        }

        private AsteroidBehaviour CreateStack(EAsteroidType type)
        {
            Color color = Color.magenta;
            _settings.ColorMaterials?.TryGetValue(type, out color);
            AsteroidBehaviour element = _poolingSystem.InstantiateAPS(_settings.ElementPoolKey).GetComponent<AsteroidBehaviour>();
            element.Configure(type, color);
            
            return element;
        }

        [Serializable]
        public class Settings
        {
            public string ElementPoolKey = "Asteroid";
            public AsteroidColorDictionary ColorMaterials;
        }

        [Serializable]
        public class AsteroidColorDictionary : SerializableDictionaryBase<EAsteroidType, Color>
        {
        }
    }
}