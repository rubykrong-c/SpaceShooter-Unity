using Code.Gameplay.Core;
using Code.Gameplay.Core.FSM.Installers;
using Code.Gameplay.Core.Input;
using Code.Gameplay.Core.Ship;
using Code.Gameplay.Core.Ship.Bullet;
using Code.Gameplay.Core.Signals;
using Code.Gameplay.UI.Screens.GameplayCoreScreen;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.Installers
{
    public class GameplayInstaller : MonoInstaller
    {

        [SerializeField] private ShipView _shipPrefab;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GameplayController>().AsSingle();
            Container.BindInterfacesAndSelfTo<CollisionHandler>().AsSingle();
            
            InstallSignals();
            InstallUI();
            InstallStates();
            InstallLevel();
            InstallAsteroids();
            InstallShip();
        }

        private void InstallShip()
        {
            Container.BindFactory<ShipView, ShipView.Factory>().FromComponentInNewPrefab(_shipPrefab)
                .WithGameObjectName("Ship").AsSingle();
            
            Container.BindInterfacesAndSelfTo<ShipController>().AsSingle();
            Container.BindInterfacesAndSelfTo<ShipWeaponController>().AsSingle();
            Container.BindInterfacesAndSelfTo<BulletSpawner>().AsSingle();
            Container.BindFactory<LinearForwardMovement, LinearForwardMovement.Factory>().AsTransient();
            Container.Bind<ShipModel>().AsSingle();
        }

        private void InstallAsteroids()
        {
            Container.BindFactory<LinearDownMovement, LinearDownMovement.Factory>().AsTransient();
            Container.BindFactory<SinusMovement, SinusMovement.Factory>().AsTransient();
            Container.BindInterfacesAndSelfTo<AsteroidMovementResolver>().AsSingle();
        }


        private void InstallSignals()
        {
            Container.DeclareSignal<GameplaySignals.OnCurrentLevelCompleted_Debug>().OptionalSubscriber();
            Container.DeclareSignal<GameplaySignals.OnCurrentLevelFailed_Debug>().OptionalSubscriber();
            Container.DeclareSignal<GameplaySignals.OnExitGameplay_Debug>().OptionalSubscriber();
            Container.DeclareSignal<GameplaySignals.OnCurrentLevelFailed>().OptionalSubscriber();
        }
        
        private void InstallUI()
        {
            GameplayCoreScreenInstaller.Install(Container);
        }
        
        private void InstallStates()
        {
            GameplayStateMachineStatesInstaller.Install(Container);
        }

        private void InstallLevel()
        {
            Container.BindInterfacesAndSelfTo<LevelLoaderController>().AsSingle();
            Container.BindInterfacesAndSelfTo<AsteroidSpawner>().AsSingle();
        }
    }
}