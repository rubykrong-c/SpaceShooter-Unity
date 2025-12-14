using Code.Base.States;

namespace Code.Gameplay.Core.FSM.Base
{
    public class GameplayStateMachineStateTransition : StateTransition<EGameplayStateMachineState, EGameplayStateMachineCommand>
    {
        public GameplayStateMachineStateTransition(EGameplayStateMachineState currentState, EGameplayStateMachineCommand currentCommand) : base(currentState, currentCommand)
        {
        }
    }

}