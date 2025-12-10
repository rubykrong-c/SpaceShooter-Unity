using Code.Base.MVP;

namespace Code.MainMenu.UI.MainMenuScreen
{
    public class MainMenuScreenPresenterFactory
    {
        private readonly MainMenuScreenPresenter.Factory _factory;

        public MainMenuScreenPresenterFactory(MainMenuScreenPresenter.Factory factory)
        {
            _factory = factory;
        }

        public BasePresenter Create(IMainMenuScreenView view)
        {
            return _factory.Create(view);
        }
    }
}