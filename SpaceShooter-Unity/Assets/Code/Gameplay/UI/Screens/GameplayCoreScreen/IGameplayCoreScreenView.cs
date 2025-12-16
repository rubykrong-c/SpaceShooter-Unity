using System;

namespace Code.Gameplay.UI.Screens.GameplayCoreScreen
{
    public interface IGameplayCoreScreenView
    {
        event Action OnWinButtonPressed;
        event Action OnLoseButtonPressed;
        event Action OnExitButtonPressed;
        void ActiveScreen(bool active);
        void RedrawHpText(string text);
    }
}