using System;
using Code.Application.FSM.States;
using Code.Application.Managers;
using Code.Application.Signals;
using Code.Application.States;
using Code.Base.States;
using Code.Levels;
using Code.Services;
using Zenject;

namespace Code.Application.Installers
{
 public class ApplicationInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            InstallApplication();
            InstallStates();
            InstallSignals();
            InstallLevels();
            InstallModel();
            InstallUtils();
            InstallApplicationScreen();

            // ServicesInstaller.Install(Container);
            // DatabaseInstaller.Install(Container);
        }
        

        private void InstallLevels()
        {
            Container.Bind<ILevelParamsGenerator>()
                .To<GeneratorLevels>()
                .AsSingle();
        }

        private void InstallApplication()
        {
            Container.Bind(typeof(IInitializable), typeof(IDisposable))
               .To<ApplicationStatesController>()
               .AsSingle();

            Container.Bind<ApplicationConditionManager>()
                .ToSelf()
                .AsSingle()
                .WhenInjectedInto<ApplicationStatesController>();
            
            Container.Bind<SceneLoader>().AsSingle();
            
            Container.Bind(typeof(IInitializable))
                .To<ApplicationController>()
                .AsSingle();
            
            Container.Bind<LoadingBarAdapter>().AsSingle();
        }

        private void InstallStates()
        {
            Container.BindFactory<IBaseState, LoadingState.Factory>()
                .To<LoadingState>()
                .WhenInjectedInto<ApplicationStatesFactory>();

            Container.BindFactory<IBaseState, MainMenuState.Factory>()
                .To<MainMenuState>()
                .WhenInjectedInto<ApplicationStatesFactory>();

            Container.BindFactory<IBaseState, GamePlayState.Factory>()
                .To<GamePlayState>()
                .WhenInjectedInto<ApplicationStatesFactory>();

            Container.Bind<ApplicationStatesFactory>().ToSelf().AsSingle();
        }

        private void InstallSignals()
        {
            SignalBusInstaller.Install(Container);

            InstallApplicationSignals();
            InstallMainMenuStateSignals();
        }

        private void InstallApplicationSignals()
        {
            Container.DeclareSignal<ApplicationSignals.OnApplicationLoaded>().OptionalSubscriber();
            Container.DeclareSignal<ApplicationSignals.OnStartGameplay>().OptionalSubscriber();
            Container.DeclareSignal<ApplicationSignals.OnBackToPreviousState>().OptionalSubscriber();

        }

        // это наверно удалю
        private void InstallMainMenuStateSignals()
        {
             Container.DeclareSignal<MainMenuStateSignals.OnShowMainMenu>().OptionalSubscriber();
            // Container.DeclareSignal<MainMenuStateSignals.OnHideMainMenu>().OptionalSubscriber();
            // Container.DeclareSignal<MainMenuStateSignals.OnActiveMainMenuInteraction>().OptionalSubscriber();
        }

    
        private void InstallModel()
        {
            /*
            Container.Bind<ApplicationModel>().ToSelf().AsSingle();
            Container.Bind(typeof(IGameplayTransactionModelGetter), typeof(IGameplayTransactionModelSetter))
                .To<GameplayTransactionModel>()
                .AsSingle();
            Container.Bind(typeof(IUserStatusGetter), typeof(IUserStatusSetter)).To<UserStatusModel>().AsSingle();
            */
        }

        // НЕ НАДО
        private void InstallUtils()
        {
            // Container.Bind<UnityMainThreadDispatcher>()
            //    .ToSelf()
            //    .FromNewComponentOnNewGameObject()
            //    .WithGameObjectName("UnityMainThreadDispatcher")
            //    .AsSingle()
            //    .NonLazy();
        }

        private void InstallApplicationScreen()
        {
            // Container.Bind<ApplicationScreenAdapter>().AsSingle();
        }
    }
}