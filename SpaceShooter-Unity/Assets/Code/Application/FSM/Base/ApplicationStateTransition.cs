using Code.Base.States;

namespace Code.Application.States
{
    public class ApplicationStateTransition : StateTransition<EApplicationState, ApplicationCommand>
    {
        public ApplicationStateTransition(EApplicationState currentState,
            ApplicationCommand currentCommand) : base(currentState, currentCommand)
        {
        }
    }
}