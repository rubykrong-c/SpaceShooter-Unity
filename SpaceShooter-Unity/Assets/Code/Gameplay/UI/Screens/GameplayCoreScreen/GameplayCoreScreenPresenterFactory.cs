using Code.Base.MVP;

namespace Code.Gameplay.UI.Screens.GameplayCoreScreen
{
    public class GameplayCoreScreenPresenterFactory
    {
        private readonly GameplayCoreScreenPresenter.Factory _factory;

        public GameplayCoreScreenPresenterFactory(GameplayCoreScreenPresenter.Factory factory)
        {
            _factory = factory;
        }

        public BasePresenter Create(IGameplayCoreScreenView view)
        {
            return _factory.Create(view);
        }
    }
}