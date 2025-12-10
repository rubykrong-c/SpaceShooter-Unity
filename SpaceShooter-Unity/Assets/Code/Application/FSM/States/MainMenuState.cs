using Code.Application.Managers;
using Code.Base.States;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using Zenject;

namespace Code.Application.FSM.States
{
    public class MainMenuState : IBaseState
    {
    
        private readonly SceneLoader _sceneLoader;

        public MainMenuState(SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        public async UniTask OnEnter()
        {
            await _sceneLoader.LoadSceneAndUnloadLoading(EScene.MAINMENU);
        }

        public async UniTask OnExit()
        {
            await _sceneLoader.UnloadSceneAndLoadLoading(EScene.MAINMENU);
        }

        #region Factory

        public class Factory : PlaceholderFactory<IBaseState>
        {
        }

        #endregion Factory
    }
}