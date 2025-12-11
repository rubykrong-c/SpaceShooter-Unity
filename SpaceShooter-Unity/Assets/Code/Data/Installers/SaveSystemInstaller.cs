using Code;
using Zenject;

public class SaveSystemInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        // Сервисы инфраструктуры
        Container.Bind<IStorageService>()
            .To<FileStorageService>()
            .AsSingle();

        Container.Bind<ISerializationService>()
            .To<UnityJsonSerializationService>()
            .AsSingle();

        Container.Bind<ISaveManager>()
            .To<SaveManager>()
            .AsSingle();

        // Доменные сервисы, которые сохраняются
        Container.BindInterfacesAndSelfTo<PlayerProgressService>()
            .AsSingle();   
        
        // Любой новый сервис, который реализует ISaveable,
        // достаточно привязать так же:
        // Container.BindInterfacesAndSelfTo<GameSettingsService>().AsSingle();
    }
}