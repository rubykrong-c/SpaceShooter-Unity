using System;
using System.Collections.Generic;
using Code.Gameplay.Core;

namespace Code.Levels
{
    [Serializable]
    public class LevelParams
    {
        public float Duration;
        public List<SubLevels> SubLevels;
    }

    [Serializable]
    public class SubLevels
    {
        public EAsteroidType AsteroidType;
        public int Count;
    }
}