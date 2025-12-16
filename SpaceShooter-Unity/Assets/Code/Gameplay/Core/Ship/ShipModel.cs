using System;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.Core.Ship
{
    public class ShipModel
    {
        public event Action<int> HpChanged;
        public int CurrentHp => _currentHp;
        
        private readonly Settings _settings;
        private int _currentHp;
        
        public ShipModel(Settings settings)
        {
            _settings = settings;
            _currentHp = _settings.MaxHp;
        }

        public void DecreaseHp()
        {
            _currentHp--;
            HpChanged?.Invoke(_currentHp);
        }
        
        [Serializable]
        public class Settings
        {
            public int MaxHp;
        }
        
    }
}