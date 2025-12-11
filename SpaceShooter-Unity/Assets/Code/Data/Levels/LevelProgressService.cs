using System;
using System.Collections.Generic;

namespace Code.Levels
{
    public class LevelProgressService : ISaveable
    {
        public string SaveKey => "level_progress";
        public Type DataType => typeof(LevelProgressSaveData);

        private LevelProgressSaveData _levelsData = new LevelProgressSaveData();
        
        public LevelProgressSaveData LevelsData => _levelsData;

        public LevelData GetLevel(int id)
        {
            var level = _levelsData.LevelsData.Find(x => x.Id == id);

            return level;
        }

        public void AddLevelData(LevelData data)
        {
            //TODO: add return bool success
            var id = data.Id;
            var lvl = _levelsData.LevelsData.Find(x => x.Id == id);
            if (lvl == null)
            {
                _levelsData.LevelsData.Add(data);
            }
        }

        // Снимок состояния для сохранения
        public object CaptureState()
        {
            return new LevelProgressSaveData()
            {
                LevelsData = new List<LevelData>(_levelsData.LevelsData)
            };
        }

        // Восстановление состояния из DTO
        public void RestoreState(object state)
        {
            if (state is LevelProgressSaveData data)
            {
                _levelsData = new LevelProgressSaveData
                {
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