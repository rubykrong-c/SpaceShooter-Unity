using System;
using System.Linq;
using Code.Application.Signals;
using Code.Gameplay.Core.Signals;
using Code.Levels;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.Core
{
    public class GameplayController : IInitializable, IDisposable
    {
        private readonly SignalBus _signals;
        private readonly ILevelProgressWriter _levelProgressWriter;
        private readonly ILevelProgressReader _levelProgressReader;

        public GameplayController(SignalBus signalBus,
            ILevelProgressWriter levelProgressWriter,
            ILevelProgressReader levelProgressReader)
        {
            _signals = signalBus;
            _levelProgressWriter = levelProgressWriter;
            _levelProgressReader = levelProgressReader;
            
            _signals.Subscribe<GameplaySignals.OnCurrentLevelCompleted_Debug>(HandleLevelCompletedCondition);
            _signals.Subscribe<GameplaySignals.OnCurrentLevelFailed_Debug>(HandleCurrentLevelFailed);
            _signals.Subscribe<GameplaySignals.OnExitGameplay_Debug>(HandleExitFromGameplay);
        }

        public void Initialize()
        {
            LogLevelParam();
        }

        public void Dispose()
        {
            _signals.TryUnsubscribe<GameplaySignals.OnCurrentLevelCompleted_Debug>(HandleLevelCompletedCondition);
            _signals.TryUnsubscribe<GameplaySignals.OnCurrentLevelFailed_Debug>(HandleCurrentLevelFailed);
            _signals.TryUnsubscribe<GameplaySignals.OnExitGameplay_Debug>(HandleExitFromGameplay);

        }
        
        private void LogLevelParam()
        {
            var param = _levelProgressReader.GetParamCurrentLevel();
            var subLevelsInfo = (param.SubLevels == null || param.SubLevels.Count == 0)
                ? "none"
                : string.Join(", ", param.SubLevels.Select(s => $"{s.AsteroidType}:{s.Count}"));
            Debug.Log($"[Gameplay] Level params => rate : {param.Rate}, subLevels: {subLevelsInfo}");
        }
        
        private async void HandleLevelCompletedCondition()
        {
            _levelProgressWriter.CompleteCurrentLevelAndGenerateNext();
            Debug.Log("Win debug");
        }

        private async void HandleCurrentLevelFailed()
        {
            await UniTask.Delay(550);
            Debug.Log("Lose debug");
        }
        
        private void HandleExitFromGameplay()
        {
            _signals.TryFire<ApplicationSignals.OnBackToPreviousState>();
            Debug.Log("Exit From gameplay");
        }

       
    }
}
