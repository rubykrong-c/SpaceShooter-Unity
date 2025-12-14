using Code.Base.MVP;
using Code.Base.Screens;
using Code.Gameplay.Core.Signals;
using Zenject;

namespace Code.Gameplay.UI.Screens.GameplayCoreScreen
{
    public class GameplayCoreScreenPresenter: BasePresenter, IScreenPresenter
    {
     
        private readonly IGameplayCoreScreenView _view;
        private readonly SignalBus _signals;
        
        public bool IsActive { get; private set; }
        
        public GameplayCoreScreenPresenter(
            IGameplayCoreScreenView view,
            SignalBus signals)
        {
            _view = view;
            _signals = signals;
        }
        
        public async void ActiveScreen()
        {
            _view.ActiveScreen(true);
            IsActive = true;
        }

        public void DeActiveScreen(bool toDisable = false)
        {
            _view.ActiveScreen(false);
            IsActive = false;
        }
        
        protected override void SubscribeOnViewEvents()
        {
            _view.OnWinButtonPressed += OnWin;
            _view.OnLoseButtonPressed += OnLose;
            _view.OnExitButtonPressed += OnExit;
        }

        protected override void UnsubscribeOnViewEvents()
        {
            _view.OnWinButtonPressed -= OnWin;
            _view.OnLoseButtonPressed -= OnLose;
            _view.OnExitButtonPressed -= OnExit;
        }
        
        private void OnWin()
        {
            _signals.TryFire<GameplaySignals.OnCurrentLevelCompleted_Debug>();
        }

        private void OnLose()
        {
            _signals.TryFire<GameplaySignals.OnCurrentLevelFailed_Debug>();
        }
        
        private void OnExit()
        {
            _signals.TryFire<GameplaySignals.OnExitGameplay_Debug>();
        }
        
        
        #region Factory

        public class Factory : PlaceholderFactory<IGameplayCoreScreenView, BasePresenter>
        {
        }

        #endregion Factory
    }
}