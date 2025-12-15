using Code.Base.Pool;
using UnityEngine;

namespace Code.Gameplay.Core
{
    public interface ILevelLoader
    {
        void StartLevel();
        void StopLevel();
        void DestroyAsteroid(GameObject asteroid);
    }
}