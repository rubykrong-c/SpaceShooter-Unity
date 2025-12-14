using System;
using System.Threading;
using Code.Gameplay.Core.FSM.Base;
using Code.Gameplay.Core.Signals;
using Code.Levels;
using Cysharp.Threading.Tasks;
using Zenject;

namespace Code.Gameplay.Core
{
    public class LevelLoaderController: IDisposable, ILevelLoader
    {
        private readonly SignalBus _signals;
        private readonly ILevelProgressReader _progress;
        private readonly IAsteroidSpawner _spawner;
        private CancellationTokenSource _cts;

        public LevelLoaderController(
            SignalBus signals,
            ILevelProgressReader progress,
            IAsteroidSpawner spawner)
        {
            _signals = signals;
            _progress = progress;
            _spawner = spawner;
        }
        
        public void StartLevel()
        {
            StopLevel();
            _cts = new CancellationTokenSource();
            LoadCurrentLevelAsync(_cts.Token).Forget();
        }
        
        public void StopLevel()
        {
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = null;
        }

        public void Dispose()
        {
            StopLevel();
        }

        private async UniTask LoadCurrentLevelAsync(CancellationToken token)
        {
            var level = _progress.GetParamCurrentLevel(); // данные из LevelProgressService
            foreach (var sub in level.SubLevels)
            {
                for (int i = 0; i < sub.Count; i++)
                {
                    token.ThrowIfCancellationRequested();
                    _spawner.Spawn(sub.AsteroidType);
                    await UniTask.Delay(TimeSpan.FromSeconds(level.Rate), cancellationToken: token);
                }
            }

            _signals.TryFire<GameplaySignals.OnCurrentLevelCompleted_Debug>();
        }
    }
}