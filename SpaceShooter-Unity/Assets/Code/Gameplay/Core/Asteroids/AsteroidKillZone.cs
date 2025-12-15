using UnityEngine;
using Zenject;
using IPoolable = Code.Base.Pool.IPoolable;

namespace Code.Gameplay.Core
{
    public class AsteroidKillZone : MonoBehaviour
    {
        private ILevelLoader _levelLoader;

        [Inject]
        private void Construct(ILevelLoader levelLoader)
        {
            _levelLoader = levelLoader;
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            var poolable = other.GetComponent<IPoolable>();
            if (poolable != null)
            {
                _levelLoader.DestroyAsteroid(other.gameObject);
            }
        }
    }
}