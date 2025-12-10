using Code.Base.MVP;
using Zenject;

namespace Code.MainMenu.UI.MainMenuScreen
{
    public class MainMenuScreenInstaller : Installer<MainMenuScreenInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindFactory<IMainMenuScreenView, BasePresenter, MainMenuScreenPresenter.Factory>()
                .To<MainMenuScreenPresenter>()
                .WhenInjectedInto<MainMenuScreenPresenterFactory>();

            Container.Bind<MainMenuScreenPresenterFactory>()
                .ToSelf()
                .AsSingle()
                .WhenInjectedInto(typeof(IMainMenuScreenView));
        }
    }
}