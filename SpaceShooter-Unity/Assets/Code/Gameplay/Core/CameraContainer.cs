using UnityEngine;
using Zenject;

namespace Code.Gameplay.Core
{
    public class CameraContainer : MonoBehaviour
    {

        [SerializeField] private Camera _camera;

        public Camera GetCamera()
        {
            return _camera;
        }
    }
}