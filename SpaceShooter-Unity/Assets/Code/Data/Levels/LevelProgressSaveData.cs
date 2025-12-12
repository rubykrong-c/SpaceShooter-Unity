using System;
using System.Collections.Generic;

namespace Code.Levels
{
    [Serializable]
    public class LevelProgressSaveData
    {
        public int CurrentLvl;
        public  List<LevelData> LevelsData = new List<LevelData>();
    }
}