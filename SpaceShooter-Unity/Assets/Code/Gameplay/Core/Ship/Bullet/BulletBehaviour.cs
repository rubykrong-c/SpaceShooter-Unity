using System;
using UnityEngine;
using Zenject;
using IPoolable = Code.Base.Pool.IPoolable;

namespace Code.Gameplay.Core.Ship.Bullet
{
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class BulletBehaviour : MonoBehaviour, IPoolable
    {
        private IBulletMovement _movement;
        private Action<GameObject>  _onHit;
        
        public void Configure(IBulletMovement movement, float speed, Vector2 minBounds, Vector2 maxBounds, Vector2 direction, Action<GameObject> despawn)
        {
            _movement = movement;
            Action returnToPool = () => despawn(this.gameObject);
            _movement.Start(new BulletContext(transform, speed, minBounds, maxBounds, direction), returnToPool); //TODO: despawn не тут должен быть
        }
        
        public void SetCollisionHandler(Action<GameObject> onHit)
        {
            if (_onHit != null)
                return;

            _onHit = onHit;
        }

        public void Dispose()
        {
            _movement?.Stop();
        }
    
        private void Update()
        {
            _movement?.Tick(Time.deltaTime);
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponent<AsteroidBehaviour>() == null)
            {
                return;
            }

            _onHit?.Invoke(this.gameObject);            
        }

        public void Initilize() { }
    }

}