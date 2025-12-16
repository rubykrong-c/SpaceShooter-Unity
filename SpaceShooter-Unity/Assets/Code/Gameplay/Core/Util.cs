using UnityEngine;

namespace Code.Gameplay.Core
{
    public static class Util
    {
        public static (Vector2, Vector2) CacheCameraBounds(Camera cam, Transform transform)
        {
            var z = Mathf.Abs(cam.transform.position.z - transform.position.z);
            var minBounds = cam.ViewportToWorldPoint(new Vector3(0, 0, z));
            var maxBounds = cam.ViewportToWorldPoint(new Vector3(1, 1, z));

            return (minBounds, maxBounds);
        }
        
        public static (Vector2, Vector2) CacheCameraBounds(Camera cam, Vector3 pos)
        {
            var z = Mathf.Abs(cam.transform.position.z - pos.z);
            var minBounds = cam.ViewportToWorldPoint(new Vector3(0, 0, z));
            var maxBounds = cam.ViewportToWorldPoint(new Vector3(1, 1, z));

            return (minBounds, maxBounds);
        }
    }
}