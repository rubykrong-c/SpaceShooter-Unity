using System;
using Code.Base.States;
using Zenject;

namespace Code.Gameplay.Core.FSM.Base
{
    public class GameplayStateMachineStatesController : StateController, IDisposable, IInitializable
    {
        private readonly SignalBus _signals;
        private readonly GameplayStateMachineConditionManager _conditionManager;
        private readonly GameplayStateMachineStatesFactory _statesFactory;

        public GameplayStateMachineStatesController(GameplayStateMachineConditionManager conditionManager,
                                                    SignalBus signals,
                                                    GameplayStateMachineStatesFactory statesFactory)
        {
            _conditionManager = conditionManager;
            _signals = signals;
            _statesFactory = statesFactory;

            Subscribe();
        }

        public void Initialize()
        {
            SetStartState();
        }

        private void SetStartState()
        {
            EGameplayStateMachineState startState = _conditionManager.GetStartState();
            SetNextState(startState);
        }

        private void SetNextStateByCommand(EGameplayStateMachineCommand command)
        {
            EGameplayStateMachineState state = _conditionManager.MoveNext(command);
            if (state != EGameplayStateMachineState.NULL)
                SetNextState(state);
        }

        private void SetNextState(EGameplayStateMachineState gameState)
        {
            IBaseState baseState = _statesFactory.GetState(gameState);
            SetState(baseState);
        }

        private void TryToSetNextStateWithCoreGameplayStartedCommand()
        {
            SetNextStateByCommand(EGameplayStateMachineCommand.CORE_GAMEPLAY_STARTED);
        }

        private void TryToSetNextStateWithCoreGameplayFinishedCommand()
        {
            SetNextStateByCommand(EGameplayStateMachineCommand.CORE_GAMEPLAY_FINISHED);
        }

        private void Subscribe()
        {
            _signals.Subscribe<GameplayStateMachineStatesSignals.OnCoreGameplayStarted>(TryToSetNextStateWithCoreGameplayStartedCommand);
            _signals.Subscribe<GameplayStateMachineStatesSignals.OnCoreGameplayFinished>(TryToSetNextStateWithCoreGameplayFinishedCommand);
        }

        private void Unsubscribe()
        {
            _signals.TryUnsubscribe<GameplayStateMachineStatesSignals.OnCoreGameplayStarted>(TryToSetNextStateWithCoreGameplayStartedCommand);
            _signals.TryUnsubscribe<GameplayStateMachineStatesSignals.OnCoreGameplayFinished>(TryToSetNextStateWithCoreGameplayFinishedCommand);
        }

        public void Dispose()
        {
            Unsubscribe();
        }
    }

}