using System;

namespace Code
{
    public class PlayerProgressService : ISaveable
    {
        public string SaveKey => "player_progress";
        public Type DataType => typeof(PlayerProgressData);

        private int _level;
        private int _coins;

        public int Level => _level;
        public int Coins => _coins;

        public void SetLevel(int level)
        {
            _level = level;
        }

        public void AddCoins(int amount)
        {
            _coins += amount;
        }

        // Снимок состояния для сохранения
        public object CaptureState()
        {
            return new PlayerProgressData
            {
                Level = _level,
                Coins = _coins
            };
        }

        // Восстановление состояния из DTO
        public void RestoreState(object state)
        {
            if (state is PlayerProgressData data)
            {
                _level = data.Level;
                _coins = data.Coins;
            }
            else
            {
                UnityEngine.Debug.LogError(
                    $"[PlayerProgressService] Invalid state type: {state?.GetType()}");
            }
        }
    }
}