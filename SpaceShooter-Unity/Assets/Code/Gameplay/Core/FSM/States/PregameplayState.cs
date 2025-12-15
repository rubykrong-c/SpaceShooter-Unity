using Code.Base.States;
using Code.Gameplay.Core.FSM.Base;
using Code.Gameplay.Core.Input;
using Code.Gameplay.Core.Ship;
using Cysharp.Threading.Tasks;
using Zenject;

namespace Code.Gameplay.Core.FSM.States
{
    public class PregameplayState : IBaseState
    {
        private readonly SignalBus _signals;
        private readonly ILevelLoader _levelLoader;
        private readonly IShipController _shipController;
        private readonly IInputHandler _inputHandler;

        public PregameplayState(SignalBus signals,
            ILevelLoader levelLoader, 
            IShipController shipController,
            IInputHandler inputHandler)
        {
            _signals = signals;
            _levelLoader = levelLoader;
            _shipController = shipController;
            _inputHandler = inputHandler;
        }

        public async UniTask OnEnter()
        {
            UnityEngine.Debug.Log("PRE GAMEPLAY");
            _levelLoader.StartLevel();
            _shipController.SpawnShip();
            _inputHandler.SetActiveInputHandler(true);
            
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