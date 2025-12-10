using Code.Application.Signals;
using Code.Base.MVP;
using Zenject;

namespace Code.MainMenu.UI.MainMenuScreen
{
public class MainMenuScreenPresenter : BasePresenter
    {
        private readonly IMainMenuScreenView _view;
        private readonly SignalBus _signals;

        public MainMenuScreenPresenter(IMainMenuScreenView view,
                                       SignalBus signals)
        {
            _view = view;
            _signals = signals;
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
            
        }

        #region Factory

        public class Factory : PlaceholderFactory<IMainMenuScreenView, BasePresenter>
        {
        }

        #endregion Factory
    }
}