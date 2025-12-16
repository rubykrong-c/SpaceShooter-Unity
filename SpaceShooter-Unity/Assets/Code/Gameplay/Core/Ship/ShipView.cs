using System;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.Core.Ship
{
    public class ShipView: MonoBehaviour
    {
        public event Action OnHitAsteroid;
        public float HalfWidth => _renderer.bounds.extents.x;
        public float HalfHeight => _renderer.bounds.extents.y;
        
        [SerializeField] private SpriteRenderer _renderer;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponent<AsteroidBehaviour>() != null)
            {
                OnHitAsteroid?.Invoke();;
            }
        }

        #region factory
        public class Factory : PlaceholderFactory<ShipView> { }
        #endregion
    }
}