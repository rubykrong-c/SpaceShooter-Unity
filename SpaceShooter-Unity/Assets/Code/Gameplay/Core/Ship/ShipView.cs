using UnityEngine;
using Zenject;

namespace Code.Gameplay.Core.Ship
{
    public class ShipView: MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _renderer;
        public float HalfWidth => _renderer.bounds.extents.x;
        public float HalfHeight => _renderer.bounds.extents.y;
        
        #region factory
        public class Factory : PlaceholderFactory<ShipView> { }
        #endregion
    }
}