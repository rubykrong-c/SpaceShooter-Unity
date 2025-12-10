using System.Collections.Generic;
using Code.Application.FSM.States;
using Code.Base.States;
using Zenject;

namespace Code.Application.States
{
    public class ApplicationStatesFactory
    {
        private readonly Dictionary<EApplicationState, PlaceholderFactory<IBaseState>> _factories;

        public ApplicationStatesFactory(LoadingState.Factory loadingStateFactory,
            MainMenuState.Factory mainMenuStateFactory,
            GamePlayState.Factory gamePlayStateFactory)
        {
            _factories = new Dictionary<EApplicationState, PlaceholderFactory<IBaseState>>
            {
                {EApplicationState.LOADING,loadingStateFactory},
                {EApplicationState.MAIN_MENU,mainMenuStateFactory},
                {EApplicationState.GAMEPLAY,gamePlayStateFactory}
            };
        }

        public IBaseState GetState(EApplicationState state)
        {
            if (_factories.TryGetValue(state, out var factory))
            {
                return factory.Create();
            }
            return null;
        }
    }
}