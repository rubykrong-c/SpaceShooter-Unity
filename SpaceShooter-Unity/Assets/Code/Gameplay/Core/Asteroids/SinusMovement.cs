using UnityEngine;
using Zenject;

namespace Code.Gameplay.Core
{

    public class SinusMovement : IAsteroidMovement
    {
        private AsteroidContext _context;
        private float _time;
        private bool _canMove = false;

        public void Start(AsteroidContext context)
        {
            _context = context;
            _time = 0f;
            _canMove = true;
        }

        public void Tick(float deltaTime)
        {
            
            _time += deltaTime;
            var dir = Vector3.down + Vector3.right * Mathf.Sin(_time * 3f);
            if (_canMove)
            {
                _context.Transform.Translate(dir.normalized * _context.Speed * deltaTime);
            }
        }

        public void Stop()
        {
            _canMove = false;
        }
        
        #region factory
        public class Factory : PlaceholderFactory<SinusMovement> { }
        #endregion
    }
}