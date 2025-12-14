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
        private readonly ILevelProgressReader _levelProgressReader;

        public MainMenuScreenPresenter(IMainMenuScreenView view,
                                       SignalBus signals,
                                       ILevelProgressReader levelProgressReader)
        {
            _view = view;
            _signals = signals;
            _levelProgressReader = levelProgressReader;
            
            _signals.Subscribe<MainMenuStateSignals.OnShowMainMenu>(ActiveScreen);
        }

        private void ActiveScreen()
        {
            _view.SetCurrentLevel(_levelProgressReader.CurrentLevel);
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