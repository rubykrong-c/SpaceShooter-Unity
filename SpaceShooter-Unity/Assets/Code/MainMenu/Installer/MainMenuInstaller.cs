using Code.MainMenu.UI.MainMenuScreen;
using Zenject;

namespace Code.MainMenu.Installer
{
    public class MainMenuInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            InstallMainMenu();
            InstallUI();
            InstallSignals();
        }

        private void InstallMainMenu()
        {
        }

        private void InstallUI()
        {
            MainMenuScreenInstaller.Install(Container);
        }

        private void InstallSignals()
        {
        }
    }
}