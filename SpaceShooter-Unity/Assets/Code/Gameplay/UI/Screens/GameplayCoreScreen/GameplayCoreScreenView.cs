using System;
using Code.Base.MVP;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Gameplay.UI.Screens.GameplayCoreScreen
{
    public class GameplayCoreScreenView: BaseView, IGameplayCoreScreenView
    {
        public event Action OnWinButtonPressed;
        public event Action OnLoseButtonPressed;
        public event Action OnExitButtonPressed;
        
#pragma warning disable 0649

        [SerializeField] private Button _winButton;
        [SerializeField] private Button _loseButton;
        [SerializeField] private Button _exitButton;
        [SerializeField] private TMP_Text _hpText;

#pragma warning restore 0649
        

        [Inject]
        private void Construct(GameplayCoreScreenPresenterFactory presenterFactory)
        {
            Presenter = presenterFactory.Create(this);
        }
        
        public void ActiveScreen(bool active)
        {
            gameObject.SetActive(active);
        }

        public void RedrawHpText(string text)
        {
            _hpText.text = text;
        }
        
        private void WinButtonPressed()
        {
            OnWinButtonPressed?.Invoke();
        }

        private void LoseButtonPressed()
        {
            OnLoseButtonPressed?.Invoke();
        }
        
        private void ExitButtonPressed()
        {
            OnExitButtonPressed?.Invoke();
        }
        
        protected override void SubscribeOnEvents()
        {
            _winButton.onClick.AddListener(WinButtonPressed);
            _loseButton.onClick.AddListener(LoseButtonPressed);
            _exitButton.onClick.AddListener(ExitButtonPressed);
        }

        protected override void UnsubscribeOnEvents()
        {
            _winButton.onClick.RemoveListener(WinButtonPressed);
            _loseButton.onClick.RemoveListener(LoseButtonPressed);
            _exitButton.onClick.RemoveListener(ExitButtonPressed);
        }
       
    }
}