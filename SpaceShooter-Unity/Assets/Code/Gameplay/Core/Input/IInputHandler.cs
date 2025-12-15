using System;
using UnityEngine;

namespace Code.Gameplay.Core.Input
{
    public interface IInputHandler
    {
        void SetActiveInputHandler(bool active);
        void SetPointerDownCallback(Action<Vector3> callback);
        void SetPointerUpCallback(Action callback);
        void SetDragCallback(Action<Vector3> callback);
        void SetDragEndCallback(Action<Vector3> callback);
    }
}