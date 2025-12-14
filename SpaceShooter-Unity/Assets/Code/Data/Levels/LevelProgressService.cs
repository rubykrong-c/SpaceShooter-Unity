using System;
using System.Collections.Generic;

namespace Code.Levels
{
    public class LevelProgressService : ISaveable, ILevelProgressWriter, ILevelProgressReader
    {
        
        private readonly ILevelParamsGenerator _generatorLevels;
        public string SaveKey => "level_progress";
        public Type DataType => typeof(LevelProgressSaveData);
        public int CurrentLevel => _currentLevel;
        
        private LevelProgressSaveData _levelsData = new LevelProgressSaveData();
        private int _currentLevel;
        
        public LevelProgressService(ILevelParamsGenerator generatorLevels)
        {
            _generatorLevels = generatorLevels;
        }
        
        public LevelParams GetParamCurrentLevel()
        {
            return _levelsData.LevelsData[_currentLevel].Params;
        }

        public void CompleteCurrentLevelAndGenerateNext()
        {
            var currentLvl = _currentLevel;
            _currentLevel++;
            
            AddNewLevel();
            CompleteCurrentLevelAndOpenNext(currentLvl);
            GenerateParamForNextLevel(currentLvl + 1);
        }
        
        public void EnsureInitialized()
        {
            if (_levelsData.LevelsData.Count > 0)
            {
                return;
            }

            CreateFirstLevel();
            CreateSecondLevel();
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

                _currentLevel = _levelsData.CurrentLvl;
            }
            else
            {
                UnityEngine.Debug.LogError(
                    $"[LevelProgressService] Invalid state type: {state?.GetType()}");
            }
        }
        
        private void CreateSecondLevel()
        {
            var secondLevel = new LevelData();
            secondLevel.Status = ELevelStatus.CLOSED;
            _levelsData.LevelsData.Add(secondLevel);
        }

        private void CreateFirstLevel()
        {
            var firstLevel = new LevelData();
            _currentLevel = 0;
            firstLevel.Status = ELevelStatus.OPENED;
            firstLevel.Params = _generatorLevels.GenerateLevelParams(_currentLevel);
            _levelsData.LevelsData.Add(firstLevel);
        }
        
        private void AddNewLevel()
        {
            var newLevel = new LevelData();
            newLevel.Status = ELevelStatus.CLOSED;
            _levelsData.LevelsData.Add(newLevel);
        }

        private void CompleteCurrentLevelAndOpenNext(int currentLvl)
        {
            _levelsData.LevelsData[currentLvl].Status = ELevelStatus.PASSED;
            _levelsData.LevelsData[currentLvl + 1].Status = ELevelStatus.OPENED;
        }

        private void GenerateParamForNextLevel(int currentLvl)
        {
            _levelsData.LevelsData[currentLvl].Params = _generatorLevels.GenerateLevelParams(currentLvl);
        }
        
    }
}