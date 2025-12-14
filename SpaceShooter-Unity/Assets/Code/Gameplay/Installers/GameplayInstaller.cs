using Code.Gameplay.Core;
using Code.Gameplay.Core.Signals;
using Code.Gameplay.UI.Screens.GameplayCoreScreen;
using Zenject;

namespace Code.Gameplay.Installers
{
    public class GameplayInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            
            Container.BindInterfacesAndSelfTo<GameplayController>()
                .AsSingle();
            InstallSignals();
            InstallUI();
        }
        
        
        private void InstallSignals()
        {
            Container.DeclareSignal<GameplaySignals.OnCurrentLevelCompleted_Debug>().OptionalSubscriber();
            Container.DeclareSignal<GameplaySignals.OnCurrentLevelFailed_Debug>().OptionalSubscriber();
            Container.DeclareSignal<GameplaySignals.OnExitGameplay_Debug>().OptionalSubscriber();
        }
        
        private void InstallUI()
        {
            GameplayCoreScreenInstaller.Install(Container);
        }
    }
}