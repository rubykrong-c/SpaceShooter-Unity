using UnityEngine;
using Zenject;

namespace Code
{
    //Add to scene
    public class SaveManagerBehaviour : MonoBehaviour
    {
        private ISaveManager _saveManager;

        [Inject]
        public void Construct(ISaveManager saveManager)
        {
            _saveManager = saveManager;
            _saveManager.LoadAll(); // Загружаем всё при старте приложения
        }

        private void OnApplicationPause(bool pause)
        {
            if (pause)
            {
                _saveManager.SaveAll();
            }
        }

        private void OnApplicationQuit()
        {
            _saveManager.SaveAll();
        }
    }
}