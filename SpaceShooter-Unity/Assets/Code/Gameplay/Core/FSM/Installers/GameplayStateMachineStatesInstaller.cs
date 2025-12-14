using System;
using Code.Base.States;
using Code.Gameplay.Core.FSM.Base;
using Code.Gameplay.Core.FSM.States;
using Zenject;

namespace Code.Gameplay.Core.FSM.Installers
{
    public class GameplayStateMachineStatesInstaller : Installer<GameplayStateMachineStatesInstaller>
    {
        public override void InstallBindings()
        {
            InstallController();
            InstallStates();
            InstallSignals();
        }

        private void InstallController()
        {
            Container.Bind(typeof(IInitializable), typeof(IDisposable))
               .To<GameplayStateMachineStatesController>()
               .AsSingle();

            Container.Bind<GameplayStateMachineConditionManager>()
                .ToSelf()
                .AsSingle()
                .WhenInjectedInto<GameplayStateMachineStatesController>();
        }

        private void InstallStates()
        {
            InstallFactory<PregameplayState, PregameplayState.Factory>();
            InstallFactory<CoreGameplayState, CoreGameplayState.Factory>();
            InstallFactory<ResultState, ResultState.Factory>();
            

            Container.Bind<GameplayStateMachineStatesFactory>().ToSelf().AsSingle();
        }

        private void InstallFactory<T, TFactory>() where T : IBaseState
                                                   where TFactory : PlaceholderFactory<IBaseState>
        {
            Container.BindFactory<IBaseState, TFactory>()
                     .To<T>()
                     .WhenInjectedInto<GameplayStateMachineStatesFactory>();
        }

        private void InstallSignals()
        {
            Container.DeclareSignal<GameplayStateMachineStatesSignals.OnCoreGameplayStarted>().OptionalSubscriber();
            Container.DeclareSignal<GameplayStateMachineStatesSignals.OnCoreGameplayFinished>().OptionalSubscriber();
            Container.DeclareSignal<GameplayStateMachineStatesSignals.OnResultClaimed>().OptionalSubscriber();
        }
    }

}