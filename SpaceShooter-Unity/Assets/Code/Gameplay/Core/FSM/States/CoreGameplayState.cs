using Code.Application.Signals;
using Code.Base.States;
using Cysharp.Threading.Tasks;
using Zenject;

namespace Code.Gameplay.Core.FSM.States
{
    public class CoreGameplayState : IBaseState
    {

        private readonly SignalBus _signals;
        private readonly ILevelLoader _levelLoader;
        
        public CoreGameplayState(SignalBus signal, ILevelLoader levelLoader)
        {
            _signals = signal;
            _levelLoader = levelLoader;
        }
        
        public async UniTask OnEnter()
        {
            UnityEngine.Debug.Log("CORE_GAMEPLAY_STATE");
            await UniTask.DelayFrame(1);
        }

        public UniTask OnExit()
        {
            _levelLoader.StopLevel();
            // TODO: это надо перенести в Result Exit
            _signals.TryFire<ApplicationSignals.OnBackToPreviousState>();
            return UniTask.DelayFrame(1);
        }

        #region Factory

        public class Factory : PlaceholderFactory<IBaseState>
        {
        }

        #endregion Factory
    }

}