using System.Collections.Generic;
using Code.Base.States;
using Code.Gameplay.Core.FSM.States;
using Zenject;

namespace Code.Gameplay.Core.FSM.Base
{
    public class GameplayStateMachineStatesFactory
    {
        private readonly Dictionary<EGameplayStateMachineState, PlaceholderFactory<IBaseState>> _factories;

        public GameplayStateMachineStatesFactory(
            PregameplayState.Factory pregameplayStateFactory,
            CoreGameplayState.Factory coreStateFactory,
            ResultState.Factory resultStateFactory)
        {
            _factories = new Dictionary<EGameplayStateMachineState, PlaceholderFactory<IBaseState>>
            {
                {EGameplayStateMachineState.PREGAMEPLAY,pregameplayStateFactory},
                {EGameplayStateMachineState.CORE_STATE,coreStateFactory},
                {EGameplayStateMachineState.RESULT_STATE,resultStateFactory}
                
            };
        }

        public IBaseState GetState(EGameplayStateMachineState state)
        {
            if (_factories.TryGetValue(state, out var factory))
            {
                return factory.Create();
            }
            throw new System.Exception($"No state factory with state {state}");
        }
    }
}