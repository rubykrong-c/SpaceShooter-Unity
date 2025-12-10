using System;
using System.Linq;
using Code.Base.MVP;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.MainMenu.UI.MainMenuScreen
{
    public class MainMenuScreenView : BaseView, IMainMenuScreenView
    {
        
#pragma warning disable 0649
        [SerializeField] private Canvas _screenCanvas;

        [SerializeField] private Button _startGameplayButton;
#pragma warning restore 0649

        public event Action OnStartGameplayButtonPressed;

        [Inject]
        private void Construct(MainMenuScreenPresenterFactory presenterFactory)
        {
            Presenter = presenterFactory.Create(this);
        }
        
        protected override void SubscribeOnEvents()
        {
            _startGameplayButton.onClick.AddListener(() => OnStartGameplayButtonPressed?.Invoke());
        }

        protected override void UnsubscribeOnEvents()
        {
            _startGameplayButton.onClick.RemoveAllListeners();
        }
      
    }
}