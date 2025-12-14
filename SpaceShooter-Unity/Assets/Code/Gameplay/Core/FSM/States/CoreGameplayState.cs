using Code.Application.Signals;
using Code.Base.States;
using Cysharp.Threading.Tasks;
using Zenject;

namespace Code.Gameplay.Core.FSM.States
{
    public class CoreGameplayState : IBaseState
    {

        private readonly SignalBus _signals;
        
        public CoreGameplayState(SignalBus signal)
        {
            _signals = signal;
        }
        
        public async UniTask OnEnter()
        {
            UnityEngine.Debug.Log("CORE_GAMEPLAY_STATE");
            await UniTask.DelayFrame(1);
        }

        public UniTask OnExit()
        {
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