using System;
using System.Collections.Generic;

namespace Code.Levels
{
    public class LevelProgressService : ISaveable
    {
        public string SaveKey => "level_progress";
        public Type DataType => typeof(LevelProgressSaveData);
        public LevelProgressSaveData LevelsData => _levelsData;
        public int CurrentLevel => _currentLevel;

        private LevelProgressSaveData _levelsData = new LevelProgressSaveData();
        private int _currentLevel;
        
        private readonly ILevelParamsGenerator _generatorLevels;

        public LevelProgressService(ILevelParamsGenerator generatorLevels)
        {
            _generatorLevels = generatorLevels;
        }

        public LevelData GetLevel(int id)
        {
            if (_levelsData.LevelsData.Count > id)
            {
                return _levelsData.LevelsData[id];
            }
            
            return null;
        }

        public void AddLevelData(LevelData data)
        {
            _levelsData.LevelsData.Add(data); 
        }

        public void IncreaseCurrentLvl()
        {
            _currentLevel++;
        }
        
        public void EnsureInitialized()
        {
            if (_levelsData.LevelsData.Count > 0)
            {
                return;
            }

            var firstLevel = new LevelData();
            _currentLevel = 0;
            firstLevel.Status = ELevelStatus.OPENED;
            firstLevel.Params = _generatorLevels.GenerateLevelParams();
            _levelsData.LevelsData.Add(firstLevel);
        }

        // Снимок состояния для сохранения
        public object CaptureState()
        {
            return new LevelProgressSaveData()
            {
                LevelsData = new List<LevelData>(_levelsData.LevelsData),
                CurrentLvl = _currentLevel
            };
        }

        // Восстановление состояния из DTO
        public void RestoreState(object state)
        {
            if (state is LevelProgressSaveData data)
            {
                _levelsData = new LevelProgressSaveData
                {
                    CurrentLvl = data.CurrentLvl,
                    LevelsData = new List<LevelData>(data.LevelsData)
                };
            }
            else
            {
                UnityEngine.Debug.LogError(
                    $"[LevelProgressService] Invalid state type: {state?.GetType()}");
            }
        }

    }
}