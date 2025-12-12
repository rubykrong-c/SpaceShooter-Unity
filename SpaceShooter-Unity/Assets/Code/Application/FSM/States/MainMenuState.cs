using Code.Application.Installers;
using Code.Application.Managers;
using Code.Base.States;
using Code.Levels;
using Code.MainMenu.UI.MainMenuScreen;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using Zenject;

namespace Code.Application.FSM.States
{
    public class MainMenuState : IBaseState
    {
    
        private readonly SceneLoader _sceneLoader;
        private readonly SignalBus _signalBus;
        private readonly LevelProgressService _levelProgressService;

        public MainMenuState(SceneLoader sceneLoader,
            SignalBus signalBus,
            LevelProgressService levelProgressService)
        {
            _sceneLoader = sceneLoader;
            _signalBus = signalBus;
            _levelProgressService = levelProgressService;
        }

        public async UniTask OnEnter()
        {
            _levelProgressService.EnsureInitialized();
            await _sceneLoader.LoadSceneAndUnloadLoading(EScene.MAINMENU);
            _signalBus.TryFire(new MainMenuStateSignals.OnShowMainMenu());
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