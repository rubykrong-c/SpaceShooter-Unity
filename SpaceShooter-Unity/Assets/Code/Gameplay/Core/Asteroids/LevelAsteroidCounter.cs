using System;
using Code.Gameplay.Core.Signals;
using Code.Levels;
using Zenject;

namespace Code.Gameplay.Core
{
    public class LevelAsteroidCounter : IDisposable
    {
        private readonly IAsteroidSpawner _spawner;
        private readonly SignalBus _signals;

        private int _remainingAsteroids;
        private bool _isActive;

        public LevelAsteroidCounter(
            IAsteroidSpawner spawner,
            SignalBus signals)
        {
            _spawner = spawner;
            _signals = signals;

            _spawner.OnAsteroidDestroy += HandleAsteroidDestroyed;
        }

        public void Prepare(LevelParams levelParams)
        {
            _remainingAsteroids = CountAsteroids(levelParams);
            _isActive = _remainingAsteroids > 0;

            if (!_isActive)
            {
                NotifyLevelCompleted();
            }
        }

        private static int CountAsteroids(LevelParams levelParams)
        {
            if (levelParams?.SubLevels == null)
            {
                return 0;
            }

            var total = 0;
            foreach (var subLevel in levelParams.SubLevels)
            {
                total += subLevel.Count;
            }

            return total;
        }

        private void HandleAsteroidDestroyed()
        {
            if (!_isActive || _remainingAsteroids <= 0)
            {
                return;
            }

            _remainingAsteroids--;
            if (_remainingAsteroids <= 0)
            {
                _isActive = false;
                NotifyLevelCompleted();
            }
        }

        private void NotifyLevelCompleted()
        {
            _signals.TryFire<GameplaySignals.OnCurrentLevelCompleted_Debug>();
        }

        public void Dispose()
        {
            _spawner.OnAsteroidDestroy -= HandleAsteroidDestroyed;
        }
    }
}
