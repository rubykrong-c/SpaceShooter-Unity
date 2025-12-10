namespace Code.Base.States
{
    public class StateController
    {
        protected IBaseState CurrentState;

        protected async void SetState(IBaseState state)
        {
            if (CurrentState != null)
                await CurrentState.OnExit();

            if (state != null)
                await state.OnEnter();

            CurrentState = state;
        }
    }
}