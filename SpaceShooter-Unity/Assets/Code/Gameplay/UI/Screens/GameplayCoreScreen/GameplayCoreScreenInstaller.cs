using System.ComponentModel;
using Code.Base.MVP;
using Zenject;

namespace Code.Gameplay.UI.Screens.GameplayCoreScreen
{
    public class GameplayCoreScreenInstaller : Installer<GameplayCoreScreenInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindFactory<IGameplayCoreScreenView, BasePresenter, GameplayCoreScreenPresenter.Factory>()
                .To<GameplayCoreScreenPresenter>()
                .WhenInjectedInto<GameplayCoreScreenPresenterFactory>();

            Container.Bind<GameplayCoreScreenPresenterFactory>()
                .ToSelf()
                .AsSingle()
                .WhenInjectedInto(typeof(IGameplayCoreScreenView));
        }
    }
}