using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Gameplay.Core.FSM.Base
{
  public class GameplayStateMachineConditionManager
    {
        private readonly Dictionary<GameplayStateMachineStateTransition, EGameplayStateMachineState> _transitions;

        private EGameplayStateMachineState _currentState { get; set; }
        private EGameplayStateMachineState _startState;
        

        public GameplayStateMachineConditionManager()
        {
            _transitions = GetCurrentTransitionDict();
        }

        public EGameplayStateMachineState GetStartState()
        {
            _currentState = _startState;
            return _startState;
        }

        public EGameplayStateMachineState MoveNext(EGameplayStateMachineCommand command)
        {
            EGameplayStateMachineState state = GetNext(command);
            if (state != EGameplayStateMachineState.NULL)
                _currentState = GetNext(command);
            return state;
        }

        private Dictionary<GameplayStateMachineStateTransition, EGameplayStateMachineState> GetCurrentTransitionDict()
        {
            _startState = EGameplayStateMachineState.PREGAMEPLAY;
            
            return new Dictionary<GameplayStateMachineStateTransition, EGameplayStateMachineState>
            {
                { new GameplayStateMachineStateTransition(EGameplayStateMachineState.PREGAMEPLAY, EGameplayStateMachineCommand.CORE_GAMEPLAY_STARTED), EGameplayStateMachineState.CORE_STATE },

                { new GameplayStateMachineStateTransition(EGameplayStateMachineState.CORE_STATE, EGameplayStateMachineCommand.CORE_GAMEPLAY_FINISHED), EGameplayStateMachineState.RESULT_STATE }
            };

        }

        private EGameplayStateMachineState GetNext(EGameplayStateMachineCommand command)
        {
            GameplayStateMachineStateTransition transition = new GameplayStateMachineStateTransition(_currentState, command);
            
            if (!_transitions.TryGetValue(transition, out EGameplayStateMachineState nextState))
            {
                throw new Exception("Invalid transition: " + _currentState + " -> " + command);
            }
            return nextState;
        }
     
    }
}