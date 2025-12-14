using UnityEngine;
using Zenject;

namespace Code.Gameplay.Core.Settings
{
    [CreateAssetMenu(fileName = "CoreSettingsInstaller", menuName = "Installers/CoreSettingsInstaller")]
    public class CoreSettingsInstaller: ScriptableObjectInstaller<CoreSettingsInstaller>
    {
        public AsteroidSpawner.AsteroidColorDictionary ColorCodeMaterialsDict => _levelLoaderSettings.ColorMaterials;
#pragma warning disable 0649
        [SerializeField]
        private AsteroidSpawner.Settings _levelLoaderSettings;
#pragma warning restore 0649

        public override void InstallBindings()
        {
            Container.BindInstance(_levelLoaderSettings).WhenInjectedInto<AsteroidSpawner>();
        }
        
        
    }
}