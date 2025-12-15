using Code.Base.Pool;
using UnityEngine;

namespace Code.Gameplay.Core
{
    public interface IAsteroidSpawner
    {
        void Spawn(EAsteroidType type);
        void Despawn(GameObject asteroid);
    }
}