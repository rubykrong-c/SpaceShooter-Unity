using System;
using Code.Base.Pool;
using UnityEngine;

namespace Code.Gameplay.Core
{
    public interface IAsteroidSpawner
    {
        event Action OnAsteroidDestroy;
        void Spawn(EAsteroidType type);
        void Despawn(GameObject asteroid);
    }
}