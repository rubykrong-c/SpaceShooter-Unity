using Code.Gameplay.Core.Input;
using Code.Gameplay.Core.Signals;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.Core.Ship
{
    public class ShipController: IShipController, IShipPositionGetter
    {

        public Vector2 PositionWeapon => _ship.transform.position;
        
        private readonly ShipView.Factory _shipFactory;
        private readonly CameraContainer _cameraContainer;
        private readonly IInputHandler _inputHandler;
        private readonly ShipModel _shipModel;
        private readonly SignalBus _signalBus;
        
        private ShipView _ship;
        private bool _dragging;
        private Vector3 _startPointerWorld;
        private Vector3 _startShipPosition;
        private Vector2 _minBounds;
        private Vector2 _maxBounds;
        private Camera _camera;
        private bool _isInit = false;
        
        public ShipController(
        ShipView.Factory shipFactory,
        CameraContainer cameraContainer, 
        IInputHandler inputHandler,
        ShipModel shipModel,
        SignalBus signalBus)
        {
            _shipFactory = shipFactory;
            _cameraContainer = cameraContainer;
            _inputHandler = inputHandler;
            _shipModel = shipModel;
            _signalBus = signalBus;
            
            _inputHandler.SetPointerDownCallback(HandlePointerDown);
            _inputHandler.SetPointerUpCallback(HandlePointerUp);
            _inputHandler.SetDragCallback(HandleDrag);
        }

        public void SpawnShip()
        {
            if (_ship == null)
            {
                _ship = _shipFactory.Create();
                SubscribeOnViewEvents();
            }
        }
        
        private void SubscribeOnViewEvents()
        {
            _ship.OnHitAsteroid += GetDmg;
            _shipModel.HpChanged += CheckDeath;
        }

        private void UnsubscribeOnViewEvents()
        {
            _ship.OnHitAsteroid -= GetDmg;
            _shipModel.HpChanged -= CheckDeath;
        }

        private void GetDmg()
        {
            _shipModel.DecreaseHp();
        }

        private void CheckDeath(int hp)
        {
            if (hp <= 0)
            {
                DeathShip();
            }
        }

        private void DeathShip()
        {
            UnsubscribeOnViewEvents();
            _signalBus.TryFire<GameplaySignals.OnCurrentLevelFailed>();
        }
        
        private void HandlePointerDown(Vector3 screenPos)
        {
            if (!_isInit)
            {
                _camera = _cameraContainer.GetCamera();
                CacheCameraBounds();
            }
            
            _dragging = true;
            _startPointerWorld = ScreenToWorld(screenPos);
            _startShipPosition = _ship.transform.position;
        }
        
        private void HandlePointerUp()
        {
            _dragging = false;
        }
        
        private void HandleDrag(Vector3 screenPos)
        {
            if (!_dragging) return;

            var currentWorld = ScreenToWorld(screenPos);
            var delta = currentWorld - _startPointerWorld;
            var target = ClampToCamera(_startShipPosition + delta);

            _ship.transform.position = target;
        }
        
        private void CacheCameraBounds()
        {
            var bounds = Util.CacheCameraBounds(_camera, _ship.transform);
            _minBounds = bounds.Item1;
            _maxBounds = bounds.Item2;
        }

        private Vector3 ClampToCamera(Vector3 position)
        {
            position.x = Mathf.Clamp(position.x, _minBounds.x + _ship.HalfWidth, _maxBounds.x - _ship.HalfWidth);
            position.y = Mathf.Clamp(position.y, _minBounds.y + _ship.HalfHeight, _maxBounds.y - _ship.HalfHeight);
            return position;
        }

        private Vector3 ScreenToWorld(Vector3 screenPos)
        {
            var depth = Mathf.Abs(_ship.transform.position.z 
                                  - _camera.transform.position.z);
            return _camera.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, depth));
        }
        
    }
}