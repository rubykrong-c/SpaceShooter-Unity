using System.Collections.Generic;
using Code.Gameplay.Core;

namespace Code.Levels
{
    public class GeneratorLevels: ILevelParamsGenerator
    {
        public LevelParams GenerateLevelParams(int id)
        {
            var par = DebugGenerateLevelParams(id);
            return par;
        }

        private LevelParams DebugGenerateLevelParams(int id)
        {
            var par = new LevelParams();
            par.Rate = 1f + 0.01f*id;
            par.SubLevels = new List<SubLevels>();
            var subLevel = new SubLevels();
            subLevel.AsteroidType = EAsteroidType.RED;
            subLevel.Count = 2  + 1*id;
            par.SubLevels.Add(subLevel);
            subLevel = new SubLevels();
            subLevel.AsteroidType = EAsteroidType.GRAY;
            subLevel.Count = 10  + 1*id;
            par.SubLevels.Add(subLevel);
            return par;
        }
    }
}