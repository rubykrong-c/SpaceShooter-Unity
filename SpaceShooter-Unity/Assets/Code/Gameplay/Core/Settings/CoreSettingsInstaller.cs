using Code.Gameplay.Core.Ship;
using Code.Gameplay.Core.Ship.Bullet;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.Core.Settings
{
    [CreateAssetMenu(fileName = "CoreSettingsInstaller", menuName = "Installers/CoreSettingsInstaller")]
    public class CoreSettingsInstaller: ScriptableObjectInstaller<CoreSettingsInstaller>
    {
        public AsteroidSpawner.AsteroidDataDictionary DataCodeMaterialsDict => _levelLoaderSettings._dataMaterials;
#pragma warning disable 0649
        [SerializeField] private AsteroidSpawner.Settings _levelLoaderSettings;
        [SerializeField] private BulletSpawner.Settings _bulletSettings;
        [SerializeField] private ShipWeaponController.WeaponSettings _weaponSettings;
        [SerializeField] private ShipModel.Settings _shipSettings;
#pragma warning restore 0649

        public override void InstallBindings()
        {
            Container.BindInstance(_levelLoaderSettings).WhenInjectedInto<AsteroidSpawner>();
            Container.BindInstance(_bulletSettings).WhenInjectedInto<BulletSpawner>();
            Container.BindInstance(_weaponSettings).WhenInjectedInto<ShipWeaponController>();
            Container.BindInstance(_shipSettings).WhenInjectedInto<ShipModel>();
        }
        
        
    }
}