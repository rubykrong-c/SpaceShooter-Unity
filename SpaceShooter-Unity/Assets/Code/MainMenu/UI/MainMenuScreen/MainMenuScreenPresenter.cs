using Code.Application.Installers;
using Code.Application.Signals;
using Code.Base.MVP;
using Code.Levels;
using Zenject;

namespace Code.MainMenu.UI.MainMenuScreen
{
public class MainMenuScreenPresenter : BasePresenter
    {
        private readonly IMainMenuScreenView _view;
        private readonly SignalBus _signals;
        private readonly LevelProgressService _levelProgressService;

        public MainMenuScreenPresenter(IMainMenuScreenView view,
                                       SignalBus signals,
                                       LevelProgressService levelProgressService)
        {
            _view = view;
            _signals = signals;
            _levelProgressService = levelProgressService;
            
            _signals.Subscribe<MainMenuStateSignals.OnShowMainMenu>(ActiveScreen);
        }

        private void ActiveScreen()
        {
            _view.SetCurrentLevel(_levelProgressService.CurrentLevel);
        }
        
        private void TryToStartGameplay()
        {
            _signals.TryFire<ApplicationSignals.OnStartGameplay>();
        }

        protected override void SubscribeOnViewEvents()
        {
            _view.OnStartGameplayButtonPressed += TryToStartGameplay;
        }

        protected override void UnsubscribeOnViewEvents()
        {
            _view.OnStartGameplayButtonPressed -= TryToStartGameplay;
        }

        public override void Dispose()
        {
            _signals.TryUnsubscribe<MainMenuStateSignals.OnShowMainMenu>(ActiveScreen);
        }

        #region Factory

        public class Factory : PlaceholderFactory<IMainMenuScreenView, BasePresenter>
        {
        }

        #endregion Factory
    }
}