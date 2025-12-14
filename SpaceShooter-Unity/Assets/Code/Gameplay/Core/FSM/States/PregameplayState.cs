using Code.Base.States;
using Code.Gameplay.Core.FSM.Base;
using Cysharp.Threading.Tasks;
using Zenject;

namespace Code.Gameplay.Core.FSM.States
{
    public class PregameplayState : IBaseState
    {
        private readonly SignalBus _signals;
        private readonly ILevelLoader _levelLoader;

        public PregameplayState(SignalBus signals,
            ILevelLoader levelLoader)
        {
            _signals = signals;
            _levelLoader = levelLoader;
        }

        public async UniTask OnEnter()
        {
            UnityEngine.Debug.Log("PRE GAMEPLAY");
            _levelLoader.StartLevel();
            await UniTask.DelayFrame(1);
            _signals.TryFire<GameplayStateMachineStatesSignals.OnCoreGameplayStarted>();
        }

        public UniTask OnExit()
        {
            return UniTask.DelayFrame(1);
        }

        #region Factory

        public class Factory : PlaceholderFactory<IBaseState>
        {
        }

        #endregion Factory
    }

}