using System;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.Core.Ship.Bullet
{
    public class LinearForwardMovement: IBulletMovement
    {
        private BulletContext _context;
        private bool _canMove = false;
        private Action _returnToPool;
        
        public void Start(BulletContext context, Action despawn)
        {
            _context = context;
            _canMove = true;
            _returnToPool = despawn;
        }

        public void Tick(float deltaTime)
        {
            if(_canMove)
            {
                _context.Transform.Translate( _context.Direction * _context.Speed * deltaTime);
            }
            
            var pos = _context.Transform.position;
            if (pos.x < _context.MinBounds.x|| pos.x > _context.MaxBounds.x ||
                pos.y < _context.MinBounds.y || pos.y > _context.MaxBounds.y)
            {
                _returnToPool?.Invoke();
            }
        }

        public void Stop()
        {
            _canMove = false;
        }
        
        #region factory
        public class Factory : PlaceholderFactory<LinearForwardMovement> { }
        #endregion
    }
}