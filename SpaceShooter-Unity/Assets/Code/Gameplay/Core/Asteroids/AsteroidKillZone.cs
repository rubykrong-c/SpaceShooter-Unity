using UnityEngine;
using Zenject;
using IPoolable = Code.Base.Pool.IPoolable;

namespace Code.Gameplay.Core
{
    public class AsteroidKillZone : MonoBehaviour
    {
        private IAsteroidSpawner _asteroidSpawner;

        [Inject]
        private void Construct(IAsteroidSpawner asteroidSpawner)
        {
            _asteroidSpawner = asteroidSpawner;
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            var poolable = other.GetComponent<AsteroidBehaviour>();
            if (poolable != null)
            {
                _asteroidSpawner.Despawn(poolable.gameObject);
            }
        }
    }
}