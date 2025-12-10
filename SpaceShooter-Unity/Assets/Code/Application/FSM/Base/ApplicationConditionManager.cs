using System;
using System.Collections.Generic;

namespace Code.Application.States
{
    public class ApplicationConditionManager
    {
        private readonly Dictionary<ApplicationStateTransition, EApplicationState> _transitions;
        private EApplicationState _currentState { get; set; }

        public ApplicationConditionManager()
        {
            _currentState = EApplicationState.LOADING;
            _transitions = new Dictionary<ApplicationStateTransition, EApplicationState>
            {
                {new ApplicationStateTransition(EApplicationState.LOADING, ApplicationCommand.OPEN_MAIN_MENU),EApplicationState.MAIN_MENU},
                {new ApplicationStateTransition(EApplicationState.MAIN_MENU, ApplicationCommand.GAMEPLAY_START),EApplicationState.GAMEPLAY},
                {new ApplicationStateTransition(EApplicationState.LOADING, ApplicationCommand.GAMEPLAY_START),EApplicationState.GAMEPLAY},
                {new ApplicationStateTransition(EApplicationState.GAMEPLAY, ApplicationCommand.BACK_TO_PREVIOUS_STATE),EApplicationState.MAIN_MENU}
            };
        }

        public EApplicationState MoveNext(ApplicationCommand command)
        {
            EApplicationState eApplicationState = GetNext(command);
            if (eApplicationState != EApplicationState.NULL)
                _currentState = GetNext(command);
            return eApplicationState;
        }

        private EApplicationState GetNext(ApplicationCommand command)
        {
            ApplicationStateTransition transition = new ApplicationStateTransition(_currentState, command);
            if (!_transitions.TryGetValue(transition, out EApplicationState nextState))
                throw new Exception("Invalid transition: " + _currentState + " -> " + command);
            return nextState;
        }
    }
}