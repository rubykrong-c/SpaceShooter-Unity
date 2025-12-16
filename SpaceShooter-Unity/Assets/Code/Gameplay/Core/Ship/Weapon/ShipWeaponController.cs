using System;
using System.Threading;
using Code.Gameplay.Core.FSM.Base;
using Code.Gameplay.Core.Ship.Bullet;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.Core.Ship
{
public class ShipWeaponController : IShipWeaponController, IDisposable
    {
        private readonly IShipPositionGetter _shipGetter; 
        private readonly IBulletSpawner _bulletSpawner;
        private readonly WeaponSettings _settings;
        private readonly CameraContainer _cameraContainer;

        private CancellationTokenSource _cts;
        private bool _isActive;
        private Vector2 _minBound;
        private Vector2 _maxBound;

        public ShipWeaponController(
            IShipPositionGetter shipGetter,
            IBulletSpawner bulletSpawner,
            WeaponSettings settings,
            CameraContainer cameraContainer)
        {
            _shipGetter = shipGetter;
            _bulletSpawner = bulletSpawner;
            _settings = settings;
            _cameraContainer = cameraContainer;
        }

        public void Initialize()
        {
        }

        public void Dispose()
        {
            StopFireLoop();
        }

        public void StartFireLoop()
        {
            if (_isActive)
            {
                return;
            }

            InitBound();

            _isActive = true;
            _cts = new CancellationTokenSource();
            FireRoutine(_cts.Token).Forget();
        }

        private void InitBound()
        {
            var bound = Util.CacheCameraBounds(_cameraContainer.GetCamera(), _shipGetter.PositionWeapon);
            Vector2 padding = new Vector2(1f, 1f) * 0.5f;
            _minBound = bound.Item1 - padding;
            _maxBound = bound.Item2 + padding;
        }

        public void StopFireLoop()
        {
            _isActive = false;
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = null;
        }

        private async UniTaskVoid FireRoutine(CancellationToken token)
        {
            await UniTask.Delay(_settings.FirstShotDelayMs, cancellationToken: token);

            while (!token.IsCancellationRequested)
            {
                Fire();
                await UniTask.Delay(_settings.FireIntervalMs, cancellationToken: token);
            }
        }

        private void Fire()
        {
            var ship = _shipGetter.PositionWeapon;
            var offset = Vector2.up;
            var spawnPos = ship + offset;
            //float speed, Vector2 minBounds, Vector2 maxBounds
            _bulletSpawner.Spawn(spawnPos, _minBound, _maxBound, Vector2.up);
        }

        [Serializable]
        public class WeaponSettings
        {
            public int FirstShotDelayMs = 0;
            public int FireIntervalMs = 1100;
        }
    }
}