using Code.Application.Signals;
using Code.Base.States;
using Code.Gameplay.Core.Ship;
using Cysharp.Threading.Tasks;
using Zenject;

namespace Code.Gameplay.Core.FSM.States
{
    public class CoreGameplayState : IBaseState
    {

        private readonly SignalBus _signals;
        private readonly ILevelLoader _levelLoader;
        private readonly IShipWeaponController _shipWeaponController;
        
        public CoreGameplayState(
            SignalBus signal, 
            ILevelLoader levelLoader,
            IShipWeaponController shipWeaponController)
        {
            _signals = signal;
            _levelLoader = levelLoader;
            _shipWeaponController = shipWeaponController;
        }
        
        public async UniTask OnEnter()
        {
            UnityEngine.Debug.Log("CORE_GAMEPLAY_STATE");
            _shipWeaponController.StartFireLoop();
            await UniTask.DelayFrame(1);
        }

        public UniTask OnExit()
        {
            _levelLoader.StopLevel();
            _shipWeaponController.StopFireLoop();
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