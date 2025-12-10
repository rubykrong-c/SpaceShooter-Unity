using System;

namespace Code.MainMenu.UI.MainMenuScreen
{
    public interface IMainMenuScreenView
    {
        event Action OnStartGameplayButtonPressed;
    }
}