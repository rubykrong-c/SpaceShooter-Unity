using System.Collections.Generic;
using Code.Gameplay.Core;

namespace Code.Levels
{
    public class GeneratorLevels: ILevelParamsGenerator
    {
        public LevelParams GenerateLevelParams()
        {
            var par = DebugGenerateLevelParams();
            return par;
        }

        private static LevelParams DebugGenerateLevelParams()
        {
            var par = new LevelParams();
            par.Rate = 1f;
            par.SubLevels = new List<SubLevels>();
            var subLevel = new SubLevels();
            subLevel.AsteroidType = EAsteroidType.RED;
            subLevel.Count = 10;
            par.SubLevels.Add(subLevel);
            return par;
        }
    }
}