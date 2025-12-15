using UnityEngine;
using Zenject;

namespace Code.Gameplay.Core
{
    public class LinearDownMovement : IAsteroidMovement
    {
        private AsteroidContext _context;
        private bool _canMove = false;
        
        public void Start(AsteroidContext context)
        {
            _context = context;
            _canMove = true;
        }

        public void Tick(float deltaTime)
        {
            if(_canMove)
            {
                _context.Transform.Translate(Vector3.down * _context.Speed * deltaTime);
            }
        }

        public void Stop()
        {
            _canMove = false;
        }
        
        #region factory
        public class Factory : PlaceholderFactory<LinearDownMovement> { }
        #endregion
    }
}