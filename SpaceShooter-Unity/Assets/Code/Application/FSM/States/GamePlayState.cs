using System;
using Code.Application.Managers;
using Code.Base.States;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Code.Application.FSM.States
{
    public class GamePlayState : IBaseState
    {
        private readonly SceneLoader _sceneLoader;

        public GamePlayState(SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        public async UniTask OnEnter()
        {
            await _sceneLoader.LoadSceneAndUnloadLoading(EScene.CORE);
            
        }

        public async UniTask OnExit()
        {
            await _sceneLoader.UnloadSceneAndLoadLoading(EScene.CORE);
            AssetBundle.UnloadAllAssetBundles(true);
            Resources.UnloadUnusedAssets().ToUniTask().Forget();
            GC.Collect();
        }

        #region Factory

        public class Factory : PlaceholderFactory<IBaseState>
        {
        }

        #endregion Factory
    }
}