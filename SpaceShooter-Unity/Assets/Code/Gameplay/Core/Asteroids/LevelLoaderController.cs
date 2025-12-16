using System;
using System.Threading;
using Code.Gameplay.Core.FSM.Base;
using Code.Gameplay.Core.Signals;
using Code.Levels;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;
using IPoolable = Code.Base.Pool.IPoolable;

namespace Code.Gameplay.Core
{
    public class LevelLoaderController: IDisposable, ILevelLoader
    {
        private readonly SignalBus _signals;
        private readonly ILevelProgressReader _progress;
        private readonly IAsteroidSpawner _spawner;
        private readonly LevelAsteroidCounter _asteroidCounter;
        private CancellationTokenSource _cts;

        public LevelLoaderController(
            SignalBus signals,
            ILevelProgressReader progress,
            IAsteroidSpawner spawner,
            LevelAsteroidCounter asteroidCounter)
        {
            _signals = signals;
            _progress = progress;
            _spawner = spawner;
            _asteroidCounter = asteroidCounter;
        }
        
        public void StartLevel()
        {
            StopLevel();
            var level = _progress.GetParamCurrentLevel();
            _asteroidCounter.Prepare(level);
            _cts = new CancellationTokenSource();
            LoadCurrentLevelAsync(level, _cts.Token).Forget();
        }
        
        public void StopLevel()
        {
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = null;
        }

        public void DestroyAsteroid(GameObject asteroid)
        {
            _spawner.Despawn(asteroid);
        }

        public void Dispose()
        {
            StopLevel();
        }

        private async UniTask LoadCurrentLevelAsync(LevelParams level, CancellationToken token)
        {
            foreach (var sub in level.SubLevels)
            {
                for (int i = 0; i < sub.Count; i++)
                {
                    token.ThrowIfCancellationRequested();
                    _spawner.Spawn(sub.AsteroidType);
                    await UniTask.Delay(TimeSpan.FromSeconds(level.Rate), cancellationToken: token);
                }
            }
        }
    }
}
