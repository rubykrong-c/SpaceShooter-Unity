using System;

namespace Code.Levels
{
    [Serializable]
    public class LevelData
    {
        public int Id;
        public ELevelStatus Status;
        public LevelParams Params;
    }
}