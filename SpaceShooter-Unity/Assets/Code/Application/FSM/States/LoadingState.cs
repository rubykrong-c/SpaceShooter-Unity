using Code.Application.Managers;
using Code.Base.States;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using Zenject;

namespace Code.Application.FSM.States
{
    public class LoadingState : IBaseState
    {
        private readonly SceneLoader _sceneLoader;

        public LoadingState(SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        public async UniTask OnEnter()
        {
            await UniTask.CompletedTask;
        }

        public async UniTask OnExit()
        {
            await _sceneLoader.UnloadSceneAndLoadLoading(EScene.ROOT);
        }

        #region Factory

        public class Factory : PlaceholderFactory<IBaseState>
        {
        }

        #endregion Factory
    }
}